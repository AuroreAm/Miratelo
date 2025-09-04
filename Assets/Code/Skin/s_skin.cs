using UnityEngine;
using System.Collections.Generic;
using Lyra;
using System;
using System.Runtime.Serialization;

namespace Triheroes.Code
{
    [note (SysOrder.s_skin)]
    [inkedAttribute]
    public class s_skin : aria
    {
        [harmony]
        character c;

        public Transform Coord { private set; get; }
        public Animator Ani { private set; get; }
        AniExt AniExt;

        float _offsetRotY;
        public float OffsetPosY;

        public class package : ink <s_skin>
        {
            public package ( GameObject skinGameObject, Vector2 offsetPosRotY )
            {
                o.Ani = skinGameObject.GetComponent<Animator> ();
                o.AniExt = AniExt.Get (o.Ani.runtimeAnimatorController);
                o.OffsetPosY = offsetPosRotY.x;
                o._offsetRotY = offsetPosRotY.y;
            }
        }

        // animation player per layer
        Player[] _players;

        /// <summary>
        /// rotation y of the character, modify this to change to rotation of the charater graphic
        /// </summary>
        public float RotY;

        /// <summary>
        /// Actual rotY of the mesh renderer now
        /// </summary>
        public float ActualRotY { private set; get; }

        /// <summary>
        /// hardcode layer index for any type of character
        /// </summary>
        public int sword, bow, knee, r_arm, upper;

        /// <summary>
        /// is the character following the skin
        /// </summary>
        public bool RootOfCharacterTransform;
        public Vector3 SkinDir;
        public float GetSpdCurves() => Ani.GetFloat ( Hash.spd );

        protected override void harmony()
        {
            Coord = c.coord;

            CacheLayerIndex();
            CreatePlayersPerLayer();

            Ani.fireEvents = false;
            // self start
            phoenix.core.start(this);
        }

        void CacheLayerIndex()
        {
            sword = Ani.GetLayerIndex("sword");
            bow = Ani.GetLayerIndex("bow");
            knee = Ani.GetLayerIndex("knee");
            r_arm = Ani.GetLayerIndex("r_arm");
            upper = Ani.GetLayerIndex("upper");
        }

        void CreatePlayersPerLayer()
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < Ani.layerCount; i++)
            {
                if (AniExt.RealLayer[i] == true)
                    players.Add(new StatePlayer(i, Ani));
                else
                    players.Add(new SyncPlayer(i, Ani));
            }
            _players = players.ToArray();
        }

        protected override void alive()
        {
            UpdatePlayers();

            // place the skin gameobject in the character's coordinate
            Ani.transform.position = Coord.position + new Vector3(0, OffsetPosY, 0);
            Ani.transform.rotation = Quaternion.Euler(0, RotY + _offsetRotY, 0);

            ActualRotY = RotY;
        }

        void UpdatePlayers()
        {
            foreach (Player p in _players)
                p.Update();
        }

        #region Animation commands
        public float[] EventPointsOfState(term Key)
        {
            if (AniExt.States.TryGetValue(Key, out AniExt.State State))
                return State.EvPoint;
            else
                Debug.LogError("no state corresponding key in Animator controller");
            return null;
        }

        public float DurationOfState(term Key)
        {
            if (AniExt.States.TryGetValue(Key, out AniExt.State State))
                return State.Duration;
            else
                Debug.LogError("no state corresponding key in Animator controller");
            return 0;
        }

        public void PlayState( SkinAnimation skinAnimation )
        {
            if ( skinAnimation.Host == null )
            throw new InvalidDataContractException ( "FATAL ERROR in playing Animation, Skin Animation must have a host" );

            if (AniExt.States.TryGetValue( skinAnimation.Key, out AniExt.State State))
                ((StatePlayer)_players[skinAnimation.LayerIndex]).Play  (skinAnimation, State.Duration, State.EvPoint, false );
            else
                Debug.LogError("no state corresponding key in Animator controller");
        }

        public void HoldState( SkinAnimation skinAnimation )
        {
            if ( skinAnimation.Host == null )
            throw new InvalidDataContractException ( "FATAL ERROR in playing Animation, Skin Animation must have a host" );

            if (AniExt.States.TryGetValue(skinAnimation.Key, out AniExt.State State))
                ((StatePlayer)_players[skinAnimation.LayerIndex]).Play( skinAnimation, State.Duration, State.EvPoint, true );
            else
                Debug.LogError("no state corresponding key in Animator controller");
        }

        public void ActivateSyncLayer(int layerIndex)
        {
            ((SyncPlayer)_players[layerIndex]).ActivateSyncLayer();
        }

        public void DisableSyncLayer(int layerIndex)
        {
            ((SyncPlayer)_players[layerIndex]).DisableSyncLayer();
        }

        public void ControlledStop(int layerIndex)
        {
            ((StatePlayer)_players[layerIndex]).Stop();
        }

        public bool CurrentStateEqualTo(int layerIndex, term key)
        {
            if (AniExt.States.TryGetValue(key, out AniExt.State AnimationState))
                return AnimationState.Key == ((StatePlayer)_players[layerIndex]).AnimationId;
            else
                Debug.LogError("no state corresponding key in Animator controller");
            return false;
        }

        public bool IsTransitioningFrom(int layerIndex, term key)
        {
            if (AniExt.States.TryGetValue(key, out AniExt.State AnimationState))
                return ((StatePlayer)_players[layerIndex]).IsTransitioningFrom(AnimationState.Key);
            else
                Debug.LogError("no state corresponding key in Animator controller");
            return false;
        }

        #endregion

        #region Animation player
        abstract class Player
        {
            protected Animator Ani;
            protected int LayerIndex;
            public abstract void Update();
            protected float PlayTime;
        }

        sealed class StatePlayer : Player
        {
            public int AnimationId => _animationId;
            int _animationId = -1;
            int _previousAnimationId = -1;
            bool _playing;
            float _duration;
            float[] _evPoint;
            int _evId = 0;
            float _fadeDuration;
            bool _hold;

            SkinAnimation _events;

            public StatePlayer(int layerIndex, Animator Ani)
            {
                LayerIndex = layerIndex;
                this.Ani = Ani;
            }

            // NOTE: don't ever call play state inside the Abort event, because it will be called infinitely
            public void Play( SkinAnimation skinAnimation, float duration, float[] eventPoints, bool hold )
            {
                if (_animationId != skinAnimation.Key || _playing == false)
                {
                    if (_playing == false && LayerIndex != 0)
                        Ani.Play( skinAnimation.Key , LayerIndex, 0);
                    else
                        Ani.CrossFadeInFixedTime( skinAnimation.Key, skinAnimation.Fade, LayerIndex, 0f, 0 );

                    if (_playing)
                            _events.InvokeAbort ();

                    _previousAnimationId = _animationId; _animationId = skinAnimation.Key;
                    _playing = true; PlayTime = 0; _evId = 0;
                    _evPoint = eventPoints;
                    _duration = duration;
                    _fadeDuration = skinAnimation.Fade;
                    _hold = hold;

                    _events = skinAnimation;
                }
            }

            public void Stop()
            {
                if (_hold)
                {
                    PlayTime = _duration - 0.1f;
                    _hold = false;
                }
                else
                    _playing = false;
            }

            public bool IsTransitioningFrom(int Animation)
            {
                return _playing && PlayTime <= _fadeDuration && Animation == _previousAnimationId;
            }

            public override void Update()
            {
                if (_playing)
                {
                    PlayTime += Time.deltaTime;

                    // make sure the correct animation fire the events
                    int currentFiringAnimation = _animationId;
                    if (_evId < _evPoint.Length)
                        while (PlayTime > _evPoint[_evId])
                        {
                            // fire in the hole
                            switch (_evId)
                            {
                                case 0:
                                    _events.InvokeEv0 ();
                                    break;

                                case 1:
                                    _events.InvokeEv1 ();
                                    break;

                                case 2:
                                    _events.InvokeEv2 ();
                                    break;
                            }
                            if (currentFiringAnimation != _animationId)
                                return;
                            _evId++;
                            if (_evId >= _evPoint.Length)
                                break;
                        }

                    // stop if done
                    if (PlayTime >= _duration)
                    {
                        if (!_hold)
                            _playing = false;
 
                            _events.InvokeEnd ();
                    }
                }

                if (LayerIndex != 0)
                    LayerFading();
            }

            void LayerFading()
            {
                if (_playing && PlayTime < _duration - 0.1f)
                    Ani.SetLayerWeight(LayerIndex, Mathf.MoveTowards(Ani.GetLayerWeight(LayerIndex), 1, Time.deltaTime * 10));
                else if (!_playing || (!_hold && PlayTime >= _duration - 0.1f))
                    Ani.SetLayerWeight(LayerIndex, Mathf.MoveTowards(Ani.GetLayerWeight(LayerIndex), 0, Time.deltaTime * 10));
            }
        }

        sealed class SyncPlayer : Player
        {

            private enum StateEnum { off, turningON, turningOFF }
            StateEnum _state;

            public SyncPlayer(int layerIndex, Animator Ani)
            {
                LayerIndex = layerIndex;
                this.Ani = Ani;
            }

            public void ActivateSyncLayer()
            {
                _state = StateEnum.turningON;
                PlayTime = 0;
            }

            public void DisableSyncLayer()
            {
                _state = StateEnum.turningOFF;
                PlayTime = 0;
            }

            public override void Update()
            {
                switch (_state)
                {
                    case StateEnum.turningON:
                        if (PlayTime / 0.1f >= 1)
                        {
                            _state = StateEnum.off;
                            Ani.SetLayerWeight(LayerIndex, 1);
                            break;
                        }
                        Ani.SetLayerWeight(LayerIndex, PlayTime / 0.1f);
                        PlayTime += Time.deltaTime;
                        return;
                    case StateEnum.turningOFF:
                        if (PlayTime / 0.1f >= 1)
                        {
                            _state = StateEnum.off;
                            Ani.SetLayerWeight(LayerIndex, 0);
                            break;
                        }
                        Ani.SetLayerWeight(LayerIndex, 1 - (PlayTime / 0.1f));
                        PlayTime += Time.deltaTime;
                        return;
                }
            }
            #endregion
        }
    }

    
    public struct SkinAnimation
    {
        public term Key;
        public int LayerIndex;
        public aria Host { get; private set; }
        public float Fade;
        public Action End;
        public Action Abort;
        public Action Ev0;
        public Action Ev1;
        public Action Ev2;

        public SkinAnimation ( term animationKey, aria host )
        {
            Key = animationKey;
            Host = host;
            LayerIndex = 0;
            Fade = .1f;
            End = null;
            Abort = null;
            Ev0 = null;
            Ev1 = null;
            Ev2 = null;
        }
        
        public void InvokeEnd ()
        {
            if ( Host.on )
            End ?.Invoke ();
        }

        public void InvokeAbort ()
        {
            if ( Host.on )
            Abort ?.Invoke ();
        }

        public void InvokeEv0 ()
        {
            if ( Host.on )
            Ev0 ?.Invoke ();
        }

        public void InvokeEv1 ()
        {
            if ( Host.on )
            Ev1 ?.Invoke ();
        }

        public void InvokeEv2 ()
        {
            if ( Host.on )
            Ev2 ?.Invoke ();
        }
    }
}
