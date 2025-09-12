using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using Triheroes.Code.CapsuleAct;

namespace Triheroes.Code
{
    [path ("AI")]
    public class ai_dash : action
    {
        [export]
        direction direction;
        [link]
        motor motor;
        [link]
        dash dash;

        protected override void _step()
        {
            dash.set ( direction );
            if ( motor.start_act ( dash ) )
            stop ();
        }
    }
}
