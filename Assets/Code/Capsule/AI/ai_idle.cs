using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;

namespace Triheroes.Code
{
    [path ("AI")]
    public class ai_idle : action
    {
        [link]
        move move;

        [link]
        motor motor;

        protected override void _start()
        {
            motor.start_act ( move );
        }

        protected override void _step()
        {
            if (!move.on)
            stop ();
        }
    }
}
