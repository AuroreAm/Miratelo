using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("dash skill")]
    public class DS0_dash : skill_motor.First
    {
        [Depend]
        ac_dash ad;

        [Depend]
        ac_backflip ab;

        public override bool SkillValid => b.HavePix <s_capsule_character_controller> ();

        public bool Spam(direction direction)
        {
            if (direction != direction.back)
                { 
                    ad.SetDashDirection(direction);
                    return StartMotor (ad);
                }
                else
                    return StartMotor (ab);
        }
        
    }
}