using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.CapsuleAct
{
    public class backflip : act
    {
        public override priority priority => priority.action;

        [link]
        capsule capsule;
        [link]
        skin skin;
        [link]
        capsule.gravity gravity;

        delta_curve movement;
        delta_curve jump;
        float jump_height = 2f;

        protected override void _ready ()
        {
            movement = new delta_curve ( triheroes_res.curve.q (animation.backflip).curve );
            jump = new delta_curve ( triheroes_res.curve.q (animation.jump).curve );
        }

        protected override void _start ()
        {
            this.link (capsule);

            skin.play ( new skin.animation ( animation.backflip, this ) { end = stop } );

            movement.start ( 5, skin.duration (animation.backflip) );
            jump.start ( jump_height, .25f );
        }

        protected override void _step()
        {
            capsule.dir += vecteur.ldir (skin.roty,Vector3.back) * movement.tick_delta () + new Vector3(0, jump.tick_delta () , 0);

            if (jump.done && !gravity.on)
            link (gravity);
        }
    }
}
