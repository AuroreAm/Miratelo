using Lyra;
using UnityEngine;
using Triheroes.Code.Axeal;

namespace Triheroes.Code {
    public class dash : act {
        public override priority priority => priority.action;

        static term dash_animation_of(direction direction) => (direction == direction.forward) ? anim.dash_forward : (direction == direction.right) ? anim.dash_right : (direction == direction.left) ? anim.dash_left : anim.dash_back;
        public static Vector3 dir_of(direction direction) => (direction == direction.forward) ? Vector3.forward : (direction == direction.back) ? Vector3.back : (direction == direction.right) ? Vector3.right : Vector3.left;

        force_curve_data f;

        [link]
        axeal a;
        [link]
        skin skin;
        [link]
        stand stand;

        public Vector3 dir;
        direction direction;
        term dash_animation;

        float fade;

        public dash _(direction _direction) {
            direction = _direction;
            dash_animation = dash_animation_of(direction);
            dir = dir_of(direction);
            fade = 0.05f;

            return this;
        }

        public void override_animation(term animation) => dash_animation = animation;
        public void override_animation(term animation, float transitionDuration) {
            dash_animation = animation;
            fade = transitionDuration;
        }

        protected override void _ready() {
            f = new force_curve_data (.5f, res.curves.q (anim.jump) );
        }

        protected override void _start() {
            stand.use(this);
            
            f.dir = dir * 5;
            a.set_force ( f );

            skin.play(new skin.animation(dash_animation, this) { fade = fade, end = stop });
        }

        protected override void _step() {
            a.deviate_main_force (vecteur.ldir(skin.roty, dir));
            stand.rotate_skin();
        }
    }

    public enum direction { forward, right, left, back }
}