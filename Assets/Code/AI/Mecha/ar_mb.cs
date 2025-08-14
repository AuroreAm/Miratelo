using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ar_mb : reflexion
    {
        [Depend]
        t_mb_aim tma;

        [Depend]
        ai_mb_aim ama;

        protected override void Step()
        {
            if (tma.on && !ama.on)
            {
                Stage.Start(ama);
            }
        }
    }

    public class ai_mb_aim : action, IMotorHandler
    {
        [Depend]
        t_mb_aim tma;
        
        [Depend]
        s_motor sm;

        [Depend]
        ac_mb_aim ama;

        public void OnMotorEnd(motor m)
        {
            tma.Finish ();
            SelfStop ();
        }

        protected override void Start()
        {
            var Success = sm.SetState ( ama, this );

            if (!Success)
            SelfStop ();
        }

        protected override void Step()
        {
            if ( !tma.on )
            SelfStop ();
        }
    }
}
