using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("sword skill")]
    public abstract class sword_skill : skill_data
    {
        [Depend]
        s_equip se;

        public override bool SkillCondition()
        {
            return se.weaponUser is s_sword_user ;
        }
    }

    public class SS2_consecutive : sword_skill
    {
        public motor [] Combo;

        public override void Create()
        {
            Combo = new motor[3];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = new ac_slash (i);
                b.IntegratePix (motor_slash);
                Combo[i] = motor_slash;
            }
        }
    }

    public class SS3_hooker_up : sword_skill
    {
    }

    public class SS4_knocker : sword_skill
    {
    }
}