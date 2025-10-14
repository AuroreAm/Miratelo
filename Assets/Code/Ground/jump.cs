using Lyra;
using UnityEngine;
using Triheroes.Code.Axeal;

namespace Triheroes.Code {
    public class jump : act {
        public override priority priority => priority.action.with2nd();

        [link]
        axeal a;
        [link]
        skin skin;

        force_curve_data f_jump;

        float max;
        float min;
        bool done;

        public term jump_animation = anim.jump;

        protected override void _ready() {
            f_jump = new force_curve_data( .5f, res.curves.q(anim.jump), 1);
        }

        public void set(float _max, float _min) {
            max = _max;
            min = _min;
        }

        protected override void _start() {
            f_jump.dir = Vector3.up * max;
            a.set_force(f_jump);
            skin.play(new skin.animation(jump_animation, this));
            done = false;
        }


        public void move(Vector3 dir_s, float factor = walk_factor.run) {
            if (on)
                a.move (dir_s * factor);
        }

        public void stop_jump() {
            if ( a.main_force.current < (max + min) / 2)
                done = true;
        }

        protected override void _step() {
            if (done && a.main_force.current >= min) {
                a.stop_main_force();
                stop();
                return;
            }

            if (a.main_force.done)
                stop();
        }
    }
}