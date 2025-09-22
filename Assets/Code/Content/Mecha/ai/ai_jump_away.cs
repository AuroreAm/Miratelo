using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("AI")]
    public class ai_jump_away : action
    {
        [link]
        motor motor;
        
        jump_away jump;

        protected override void _ready()
        {
            jump = new jump_away ();
        }

        protected override void _start()
        {
            motor.start_act ( jump );
        }

        protected override void _step()
        {
            if (!jump.on)
            stop ();
        }
    }
}
