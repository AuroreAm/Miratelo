using UnityEngine;
using System.Collections.Generic;
using Lyra;
using System;

namespace Triheroes.Code
{
    [star(order.skin)]
    [inked]
    public class skin : star.main
    {
        [link]
        character c;

        public Vector3 position => coord.position;

        public Transform coord { private set; get; }
        public Animator ani { private set; get; }
        AniExt ext;

        float offset_roty;
        public float offset_posy;

        public class ink : ink<skin>
        {
            public ink(GameObject gameobject_of_skin, Vector2 offset_pos_rot_y)
            {
                o.ani = gameobject_of_skin.GetComponent<Animator>();
                o.ext = AniExt.get(o.ani.runtimeAnimatorController);
                o.offset_posy = offset_pos_rot_y.x;
                o.offset_roty = offset_pos_rot_y.y;
            }
        }

        // animation player per layer
        player[] players;

        /// <summary> rotation y of the character, modify this to change to rotation of the charater graphic </summary>
        public float roty;

        /// <summary> actual rotY of the mesh renderer now </summary>
        public float roty_direct { private set; get; }

        /// <summary> hardcode layer index for any type of character </summary>
        public int sword, bow, knee, r_arm, upper;


        protected override void _ready()
        {
            coord = c.coord;

            cache_layer_index();
            set_player_per_layer();

            ani.fireEvents = false;
            // self start
            phoenix.core.execute (this);
        }

        void cache_layer_index()
        {
            sword = ani.GetLayerIndex("sword");
            bow = ani.GetLayerIndex("bow");
            knee = ani.GetLayerIndex("knee");
            r_arm = ani.GetLayerIndex("r_arm");
            upper = ani.GetLayerIndex("upper");
        }

        void set_player_per_layer()
        {
            List<player> _players = new List<player>();

            for (int i = 0; i < ani.layerCount; i++)
            {
                if (ext.states_type[i] == true)
                    _players.Add(new state(i, ani));
                else
                    _players.Add(new sync(i, ani));
            }
            players = _players.ToArray();
        }

        protected override void _step()
        {
            tick_players();

            // place the skin gameobject in the character's coordinate
            ani.transform.position = coord.position + new Vector3(0, offset_posy, 0);
            ani.transform.rotation = Quaternion.Euler(0, roty + offset_roty, 0);

            roty_direct = roty;
        }

        void tick_players()
        {
            foreach (player p in players)
                p.tick();
        }

        protected override void _destroy() {
            GameObject.Destroy ( ani.gameObject );
        }

        #region Animation commands
        public float[] event_points(term key)
        {
            if (ext.states.TryGetValue(key, out AniExt.state State))
                return State.evs;
            else
                Dev.Break($"no state {key.name} key in Animator controller");
            return null;
        }

        public float duration(term key)
        {
            if (ext.states.TryGetValue(key, out AniExt.state State))
                return State.duration;
            else
                Dev.Break($"no state {key.name} key in Animator controller");
            return 0;
        }

        public void play(animation animation)
        {
            if (animation.host == null)
                throw new InvalidOperationException("FATAL ERROR in playing Animation, Skin Animation must have a host");

            if (ext.states.TryGetValue(animation.key, out AniExt.state State))
                ((state)players[animation.layer]).play(animation, State.duration, State.evs, false);
            else
                Dev.Break($"no state {animation.key.name} key in Animator controller");
        }

        public void hold(animation animation)
        {
            if (animation.host == null)
                throw new InvalidOperationException("FATAL ERROR in playing Animation, Skin Animation must have a host");

            if (ext.states.TryGetValue(animation.key, out AniExt.state State))
                ((state)players[animation.layer]).play(animation, State.duration, State.evs, true);
            else
                Dev.Break($"no state {animation.key.name} key in Animator controller");
        }

        public void enable_sync_layer(int layer)
        {
            ((sync)players[layer]).enable_sync_layer();
        }

        public void disable_sync_layer(int layer)
        {
            ((sync)players[layer]).disable_sync_layer();
        }

        public void stop(int layer)
        {
            ((state)players[layer]).stop();
        }

        public bool state_equals(int layer, term key)
        {
            if (ext.states.TryGetValue(key, out AniExt.state AnimationState))
                return AnimationState.key == ((state)players[layer]).animation_key;
            else
                Dev.Break($"no state {key.name} key in Animator controller");
            return false;
        }

        public bool transitioning_from(int layer, term key)
        {
            if (ext.states.TryGetValue(key, out AniExt.state AnimationState))
                return ((state)players[layer]).transitioning_from(AnimationState.key);
            else
                Dev.Break($"no state {key.name} key in Animator controller");
            return false;
        }

        #endregion

        #region Animation player
        abstract class player
        {
            protected Animator ani;
            protected int layer;
            public abstract void tick();
            protected float play_time;
        }

        sealed class state : player
        {
            public int animation_key => id;
            int id = -1;
            int previous = -1;
            bool playing;
            float duration;
            float[] evs;
            int ev_id = 0;
            float fade_duration;
            bool hold;

            animation ev_handler;

            public state(int _layer_index, Animator _ani)
            {
                layer = _layer_index;
                ani = _ani;
            }

            // NOTE: don't ever call play state inside the Abort event, because it will be called infinitely
            public void play(animation animation, float _duration, float[] _evs, bool _hold)
            {
                if (id != animation.key || playing == false || _hold)
                {
                    if (playing == false && layer != 0)
                        ani.Play(animation.key, layer, 0);
                    else
                        ani.CrossFadeInFixedTime(animation.key, animation.fade, layer, 0f, 0);

                    if (playing)
                        ev_handler.invoke_abort();

                    previous = id; id = animation.key;
                    playing = true; play_time = 0; ev_id = 0;
                    evs = _evs;
                    duration = _duration;
                    fade_duration = animation.fade;
                    hold = _hold;

                    ev_handler = animation;
                }
            }

            public void stop()
            {
                if (hold)
                {
                    play_time = duration - 0.1f;
                    hold = false;
                }
                else
                    playing = false;
            }

            public bool transitioning_from(int animation)
            {
                return playing && play_time <= fade_duration && animation == previous;
            }

            public override void tick()
            {
                if (playing)
                {
                    play_time += Time.deltaTime;

                    // make sure the correct animation fire the events
                    int current_ev_id = id;
                    if (ev_id < evs.Length)
                        while (play_time > evs[ev_id])
                        {
                            // fire in the hole
                            switch (ev_id)
                            {
                                case 0:
                                    ev_handler.invoke_ev0();
                                    break;

                                case 1:
                                    ev_handler.invoke_ev1();
                                    break;

                                case 2:
                                    ev_handler.invoke_ev2();
                                    break;
                            }
                            if (current_ev_id != id)
                                return;
                            ev_id++;
                            if (ev_id >= evs.Length)
                                break;
                        }

                    // stop if done
                    if (play_time >= duration)
                    {
                        if (!hold)
                            playing = false;

                        ev_handler.invoke_end();
                    }
                }

                if (layer != 0)
                    layer_fade();
            }

            void layer_fade()
            {
                if (playing && play_time < duration - 0.1f)
                    ani.SetLayerWeight(layer, Mathf.MoveTowards(ani.GetLayerWeight(layer), 1, Time.deltaTime * 10));
                else if (!playing || (!hold && play_time >= duration - 0.1f))
                    ani.SetLayerWeight(layer, Mathf.MoveTowards(ani.GetLayerWeight(layer), 0, Time.deltaTime * 10));
            }
        }

        sealed class sync : player
        {

            private enum states { off, turningON, turningOFF }
            states state;

            public sync(int _layer_index, Animator _ani)
            {
                layer = _layer_index;
                this.ani = _ani;
            }

            public void enable_sync_layer()
            {
                state = states.turningON;
                play_time = 0;
            }

            public void disable_sync_layer()
            {
                state = states.turningOFF;
                play_time = 0;
            }

            public override void tick()
            {
                switch (state)
                {
                    case states.turningON:
                        if (play_time / 0.1f >= 1)
                        {
                            state = states.off;
                            ani.SetLayerWeight(layer, 1);
                            break;
                        }
                        ani.SetLayerWeight(layer, play_time / 0.1f);
                        play_time += Time.deltaTime;
                        return;
                    case states.turningOFF:
                        if (play_time / 0.1f >= 1)
                        {
                            state = states.off;
                            ani.SetLayerWeight(layer, 0);
                            break;
                        }
                        ani.SetLayerWeight(layer, 1 - (play_time / 0.1f));
                        play_time += Time.deltaTime;
                        return;
                }
            }
            #endregion
        }

        public struct animation
        {
            public term key;
            public int layer;
            public star host { get; private set; }
            public float fade;
            public Action end;
            public Action abort;
            public Action ev0;
            public Action ev1;
            public Action ev2;

            public animation (term animationKey, star _host)
            {
                key = animationKey;
                host = _host;
                layer = 0;
                fade = .1f;
                end = null;
                abort = null;
                ev0 = null;
                ev1 = null;
                ev2 = null;
            }

            public void invoke_end ()
            {
                if (host.on)
                    end?.Invoke();
            }

            public void invoke_abort ()
            {
                if (host.on)
                    abort?.Invoke();
            }

            public void invoke_ev0 ()
            {
                if (host.on)
                    ev0?.Invoke();
            }

            public void invoke_ev1 ()
            {
                if (host.on)
                    ev1?.Invoke();
            }

            public void invoke_ev2 ()
            {
                if (host.on)
                    ev2?.Invoke();
            }
        }
    }

}
