using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category ("mecha skills")]
    public class MBS0 : skill_motor.Second
    {

        [Depend]
        ac_mb_aim ama;

        public override bool SkillValid => true;

        public bool Start ()
        {
            return StartMotor2nd (ama);
        }

        public void Stop ()
        {
            StopMotor2nd ();
        }

    }
}
