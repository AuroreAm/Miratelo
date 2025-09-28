using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    [inked]
    public class mecha_buster : moon
    {
        public class ink : ink <mecha_buster>
        {
            public ink ( Transform orig, Transform end ) 
            {
                o.orig = orig;
                o.end = end;
            }
        }

        // PLASMA BULLETS
        public const float plasma_speed = 50;
        public const float plasma_cost = .3f;
        arrow.w plasma;

        // BUSTER TRANSFORM
        Transform orig, end;
        public float roty => vecteur.rot_direction_y (orig.position, end.position);
        public Vector3 position => orig.position;
        
        // CHARGER
        float charge_speed = 1f;
        stellar.w st_charge_init;
        stellar.w st_charge_loop;
        stellar.w st_shoot;

        protected override void _ready() {
            st_charge_init = res.stellars.q (new term("charge1")).get_w ();
            st_charge_loop = res.stellars.q (new term("charge2")).get_w ();
            st_shoot = res.stellars.q (new term("charge3")).get_w ();
        }

        public class aim : act
        {
            public override priority priority => priority.sub;

            // target aim rotation
            public float roty;
            float speed => 720 * Time.deltaTime;

            [link]
            mecha_buster buster;
            [link]
            skin skin;

            protected override void _start()
            {
                skin.hold(new skin.animation(animation.begin_aim, this) { layer = skin.upper });
                at(skin.roty);
            }

            public void at (float y)
            {
                roty = y;
            }

            protected override void _stop()
            {
                skin.stop (skin.upper);
            }
        }

        public class charger : controller {

            [link]
            aim aim;

            [link]
            mecha_buster buster;
            float charge;
            public bool charged => state != state_.charge;
            public bool done => state == state_.done;

            enum state_ { charge, charged, shooting, done }
            state_ state;

            int charge_loop;

            protected override void _start() {
                state = state_.charge;
                // effect at start
                buster.st_charge_init.fire (buster.position);
                charge_loop = buster.st_charge_loop.fire (buster.position);
            }

            protected override void _step() {
                if (state == state_.charge) {
                    buster.st_charge_loop.set_position ( charge_loop, buster.position );

                    charge += buster.charge_speed * Time.deltaTime;
                    if ( charge >= 1 ) {
                        charge = 1;
                        state = state_.charged;
                    }
                }
            }

            public void shoot () {
                if (state == state_.charged) state = state_.shooting;
                    else return;

                buster.st_shoot.fire (buster.position);
                buster.plasma.fire ( buster.end.position, Quaternion.Euler (0, aim.roty,0), plasma_speed );
                charge -= plasma_cost;

                if (charge <= 0) state = state_.done;
            }
        }
    }
}