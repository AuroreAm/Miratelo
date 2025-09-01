using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("bow skill")]
    public class BS0 : skill_motor.Second
    {
        [Depend]
        s_equip se;

        [Depend]
        ac_aim aa;

        public override bool SkillValid => se.weaponUser is s_bow_user;

        public bool Start ()
        {
            return StartMotor2nd (aa);
        }

        public void Stop ()
        {
            StopMotor2nd ();
        }
    }
}
