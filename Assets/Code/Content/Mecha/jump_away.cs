using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public class jump_away : act
    {
        public override priority priority => priority.action;

        [link]
        capsule capsule;
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
}
