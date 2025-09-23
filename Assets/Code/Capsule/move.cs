using System;
using Lyra;
using UnityEngine;
using Triheroes.Code.Axeal;

namespace Triheroes.Code {
    public class move : act {
        public override priority priority => priority.def2.with2nd();

        [link]
        axeal a;
        [link]
        skin skin;
        [link]
        skin_dir skin_dir;
        [link]
        stand stand;
        [link]
        footstep footstep;

        public term state { private set; get; }

        /// <summary> walk factor of the movement, used to interpolate between idle and walk, the character's speed is multiplied by this factor </summary>
        public float factor;

        float sprint_cooldown;
        int frame;

        Vector3 dir;

        protected override void _start() {
            this.link(footstep);
            stand.use(this);

            if (frame != Time.frameCount) {
                sprint_cooldown = 0;
                dir = Vector3.zero;
                to_idle();
            }
            // don't reset anything if this is aquired/freed on the same frame
            else {
                if (state == animation.idle)
                    to_idle();
                else
                    to_run();
            }
        }

        protected override void _step() {
            set_animation();
            rotation();
            check_sprint_cooldown();
            reset();
        }

        void reset() => dir = Vector3.zero;

        protected override void _stop() {
            frame = Time.frameCount;
        }

        void set_animation() {
            // idle => run
            if (state == animation.idle && (dir.magnitude > 0.01f)) {
                to_run();
                set_animation();
                return;
            }
            // run modulation ( walk - run - sprint ) => idle
            else if (state == animation.run || state == animation.sprint || state == animation.walk) {
                if (!factor_corresponds_state(factor, state))
                    to_run();

                if (dir.magnitude < 0.01f) {
                    footstep.stop();
                    if (state == animation.sprint || sprint_cooldown > 0)
                        brake();
                    else
                        to_idle();
                }
            }
        }

        void to_idle() {
            skin.play(new skin.animation(animation.idle, this));
            state = animation.idle;
        }

        void to_run() {
            if (state == animation.sprint)
                sprint_cooldown = 0.5f;

            term Animation = (factor == walk_factor.walk) ? animation.walk : (factor == walk_factor.run) ? animation.run : animation.sprint;
            skin.play(new skin.animation(Animation, this) { fade = .2f });

            // get interval time from two footstep animation events from the clip
            footstep.play(skin.event_points(Animation)[1] - skin.event_points(Animation)[0]);

            state = (factor == walk_factor.walk) ? animation.walk : (factor == walk_factor.run) ? animation.run : animation.sprint;
        }

        void brake() {
            skin.play(new skin.animation(animation.brake, this) { end = _brake_end, fade = .05f });
            state = animation.brake;
            link(skin_dir);
            skin_dir.dir = skin.ani.transform.forward.normalized;
        }

        void _brake_end() {
            if (skin_dir.on)
                unlink(skin_dir);
            to_idle();
        }

        static bool factor_corresponds_state(float factor, term state) {
            return (factor == walk_factor.walk && state == animation.walk) || (factor == walk_factor.run && state == animation.run) || (factor == walk_factor.sprint && state == animation.sprint);
        }

        void rotation() {
            if (dir.magnitude > 0)
                stand.roty = vecteur.rot_direction_y(Vector3.zero, dir);

            // brake turn animation if rotation difference is too high // and is sprinting
            if (state == animation.sprint && Mathf.Abs(Mathf.DeltaAngle(stand.roty, skin.roty)) > 120)
                rotation_brake();

            if (state == animation.brake && Mathf.Abs(Mathf.DeltaAngle(stand.roty, skin.roty)) > 120)
                rotation_brake();

            stand.rotate_skin();
        }

        void rotation_brake() {
            skin.play(new skin.animation(animation.rotation_brake, this) { end = _rotation_brake_end, fade = .05f });
            state = animation.rotation_brake;
            if (skin_dir.on)
                unlink(skin_dir);
        }

        void _rotation_brake_end() {
            to_run();
        }

        void check_sprint_cooldown() {
            if (sprint_cooldown > 0)
                sprint_cooldown -= Time.deltaTime;
        }

        #region public methods
        /// <param name="_dir_s"> per second </param>
        public void walk(Vector3 _dir_s, float _factor = walk_factor.run) {
            if (on) {
                dir += _dir_s;
                factor = _factor;

                if (state != animation.rotation_brake && state != animation.idle)
                    a.walk(_dir_s * _factor);
            }
        }
        #endregion
    }


    public static class walk_factor {
        public const float idle = 0;
        public const float walk = 0.15f;
        public const float tired = 0.14f;
        public const float run = 1;
        public const float sprint = 2;
    }
}