using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("AI")]
    public class ai_dash_slash : action
    {
        [link]
        motor motor;
        
        dash_slay attack;

        protected override void _ready()
        {
            attack = new dash_slay ();
        }

        protected override void _start()
        {
            motor.start_act ( attack );
        }

        protected override void _step()
        {
            if (!attack.on)
            stop ();
        }
    }
}