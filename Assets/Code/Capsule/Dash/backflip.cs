using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code.CapsuleAct
{
    public class backflip : act
    {
        public override priority priority => priority.action;

        [link]
        skin skin;
        [link]
        axeal a;
        
        force_curve_data f_jump;
        force_curve_data f_move;
        force_curve_data[] f;
        float jump_height = 2f;

        protected override void _ready ()
        {
            f_move = new force_curve_data ( skin.duration (animation.backflip), triheroes_res.curve.q (animation.backflip) );

            f_jump = new force_curve_data ( Vector3.up * jump_height, .25f, triheroes_res.curve.q (animation.jump), 1 );

            f = new force_curve_data[] { f_move, f_jump };
        }

        protected override void _start ()
        {
            skin.play ( new skin.animation ( animation.backflip, this ) { end = stop } );

            f_move.dir = vecteur.ldir (skin.roty,Vector3.back);
            a.set_forces (f);
        }
    }
}
