using Lyra;
using UnityEngine;
using Triheroes.Code.Axeal;

namespace Triheroes.Code
{
    public class decoherence_blink : skill, act_handler {
        [link]
        stamina stamina;

        [link]
        motor motor;
        
        [link]
        dash_blink blink;

        [link]
        decoherence_air air;

        [link]
        backflip backflip;

        public bool on => true;

        public bool spam(direction direction) {
            if (stamina.has_green()) {
                if (air.on)
                phoenix.core.stop_action ( air );

                bool success;
                if (direction != direction.back)
                    success = motor.start_act(blink._(direction),this);
                else
                    success = motor.start_act(backflip,this);

                if (success)
                    stamina.use (1);

                return success;
            }

            return false;
        }

        public void prepare_jump () {
            if (active ()) {
                motor.stop_act (this);
                if ( !air.on )
                phoenix.core.start_action (air);
            }
        }

        public void spam_decoherence_air_only () {
            if ( !air.on && stamina.has_green() ) {
            phoenix.core.start_action (air);
            stamina.use (1);
            }
        }

        public bool active () {
            return motor.act == blink;
        }

        public void _act_end(act a, act_status status) {
        }
    }

    public class dash_blink : act {
        [link]
        decoherence_trail trail;
        public override priority priority => priority.action;
        dash_core core;

        protected override void _ready() {
            core = with ( new dash_core (sh.decoherence_blink) );
            core.set_animations ( anim.dash_forward, anim.dash_right, anim.dash_left, anim.dash_back );
            core.lenght = 5;
        }

        public dash_blink _ ( direction direction ) {
            core.prepare ( direction );
            return this;
        }

        protected override void _start() {
            core.start ( this, false, true, stop );
            link (trail);
        }

        protected override void _step() {
            core.skin_stand_update ();
        }
    }

    public class decoherence_trail : controller {
        [link]
        capsule capsule;
        [link]
        skin_layer graphic;
        
        float t;
        const float interval = .02f;

        after_image.w trail;

        protected override void _ready() {
            trail = res.after_image.q (sh.after_image).get_w ();
        }

        protected override void _start() {
            spark_trail ();
        }

        void spark_trail () {
            graphic.set_layers_for_capture ();
            trail.fire ( capsule.cc.transform.position + capsule.cc.center, .5f );
            graphic.restore_layers ();  
        }

        protected override void _step() {
            while (t <= 0) {
                t += interval;
                spark_trail ();
            }

            t -= Time.deltaTime;
        }
    }

    public class decoherence_air : action {
        [link]
        decoherence_trail trail;

        [link]
        actor_speed actor_speed;

        [link]
        ground ground;

        float t;

        protected override void _start() {
            link ( trail );
            actor_speed.speed += 3f;

            // small time offset before ground check
            t = 0.1f;
        }

        protected override void _step() {
            if ( t > 0 ) {
                t -= Time.deltaTime;
            } else if ( ground.raw ) {
                stop ();
            }
        }

        protected override void _stop() {
            actor_speed.speed -= 3f;
        }
    }

        /*
    public class dash_blink : act {
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

        [link]
        decoherence_trail trail;

        const float lenght = 5;
        const float duration = .4f;
        public Vector3 dir;
        direction direction;
        term dash_animation;

        float fade;

        public dash_blink _(direction _direction) {
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
            f = new force_curve_data (duration, res.curves.q ( sh.decoherence_blink ), 1 );
        }

        protected override void _start() {
            stand.use(this);
            
            f.dir = dir * lenght;
            a.set_force ( f );

            skin.play(new skin.animation(dash_animation, this) { fade = fade, end = stop });

            link (trail);
        }

        protected override void _step() {
            a.deviate_main_force (vecteur.ldir(skin.roty, dir));
            stand.rotate_skin();
        }
    }*/
}