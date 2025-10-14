using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code
{
    public class backflip : act
    {
        public override priority priority => priority.action;

        [link]
        skin skin;
        [link]
        axeal a;
        
        force_curve_data[] f;
        float jump_height = 2f;

        protected override void _ready ()
        {
            f = new force_curve_data[2];

            f[0] = new force_curve_data ( skin.duration (anim.backflip), res.curves.q (anim.backflip) );

            f[1] = new force_curve_data ( Vector3.up * jump_height, .25f, res.curves.q (anim.jump), 1 );
        }

        protected override void _start ()
        {
            skin.play ( new skin.animation ( anim.backflip, this ) { end = stop } );

            f[0].dir = vecteur.ldir (skin.roty,Vector3.back * 5f);
            a.set_forces (f);
        }
    }
}
