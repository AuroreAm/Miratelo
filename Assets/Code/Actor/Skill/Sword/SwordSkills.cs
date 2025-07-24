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

    public class SS2 : sword_skill
    {
        public static readonly term[] SlashKeys = { AnimationKey.slash_0, AnimationKey.slash_1, AnimationKey.slash_2 };

        public motor[] DefaultCombo;

        public override void Create()
        {
            DefaultCombo = new motor[3];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = new ac_slash ( SlashKeys[i] );
                b.IntegratePix (motor_slash);
                DefaultCombo[i] = motor_slash;
            }
        }
    }

    public class SS7
    {
        public static readonly term[] SlashKeys = { AnimationKey.SS7_0, AnimationKey.SS7_1, AnimationKey.SS7_2 };
    }

    public class SS3_hooker_up : sword_skill
    {
    }

    public class SS4_knocker : sword_skill
    {
    }
}