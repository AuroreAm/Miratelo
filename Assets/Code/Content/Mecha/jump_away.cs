using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using Triheroes.Code.Axeal;

namespace Triheroes.Code
{
    public class jump_away : act {
        public override priority priority => priority.action;

        [link]
        axeal a;
        [link]
        skin skin;
        [link]
        ground ground;
        [link]
        gravity gravity;

        bool done;

        force_curve_data [] f;
        float jump_height = 4f;

        float jump_out_distance = 15;
        
        readonly term land_animation = animation.fall_end;

        protected override void _ready() {
            f = new force_curve_data[2];

            f[0] = new force_curve_data ( 1, res.curves.q (animation.backflip) );

            f[1] = new force_curve_data ( Vector3.up * jump_height, .25f, res.curves.q (animation.jump), 1 );
        }

        protected override void _start() {
            done = false;
            skin.play ( new skin.animation ( animation.jump, this ) {end = jump_done} );
            
            f[0].dir = vecteur.ldir (skin.roty,Vector3.back * jump_out_distance);
            a.set_forces (f);
        }

        void jump_done () {
            done = true;
        }

        protected override void _step() {
            if ( done && ground && gravity < 0 && Vector3.Angle(Vector3.up, ground.normal) <= 45) {
                skin.play( new skin.animation( land_animation, this ) { end = stop } );
            }
        }
    }

    /*
    public class jump_away : act
    {

        public override priority priority => priority.action;

        [link]
        skin skin;
        [link]
        capsule.gravity gravity;
        
        [link]
        protected ground ground;

        delta_curve movement;
        delta_curve jump;
        float jump_height = 4f;

        protected override void _ready ()
        {
            movement = new delta_curve ( triheroes_res.curve.q (animation.backflip).curve );
            jump = new delta_curve ( triheroes_res.curve.q (animation.jump).curve );
        }

        protected override void _start ()
        {
            this.link (capsule);

            skin.play ( new skin.animation ( animation.jump, this ) );

            movement.start ( 15, 1 );
            jump.start ( jump_height, .5f );
        }

        protected override void _step()
        {
            capsule.dir += vecteur.ldir (skin.roty,Vector3.back) * movement.tick_delta () + new Vector3(0, jump.tick_delta () , 0);

            if (jump.done && !gravity.on)
            link (gravity);

            if (jump.done && ground.raw && gravity < 0 && Vector3.Angle(Vector3.up, ground.normal) <= 45)
            {
                skin.play( new skin.animation( animation.fall_end, this ) );
                stop();
            }
        }
    }
    */
}
