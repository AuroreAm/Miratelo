using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("AI")]
    public class ai_dash_slash : action, acting
    {
        [link]
        motor motor;
        
        dash_slay attack;

        public void _act_end(act m)
        {}

        protected override void _ready()
        {
            attack = new dash_slay ();
        }

        protected override void _step()
        {
            if ( motor.start_act ( attack, this ) )
            stop ();
        }
    }
}
