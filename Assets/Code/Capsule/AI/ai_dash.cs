using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using Triheroes.Code.CapsuleAct;

namespace Triheroes.Code
{
    [path ("AI")]
    public class ai_dash : action, acting
    {
        [export]
        direction direction;
        [link]
        motor motor;
        [link]
        dash dash;

        public void _act_end(act m)
        {}

        protected override void _step()
        {
            dash.set ( direction );
            if ( motor.start_act ( dash, this ) )
            stop ();
        }
    }
}
