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
        [Depend]
        s_equip se;

        [Depend]
        s_mind sm;

        [Depend]
        ac_SS2 SS2;

        [Depend]
        ac_SS2.ac_SS2_next_combo SS2_next;
    }

    public class SS3_hooker_up : sword_skill
    {
    }

    public class SS4_knocker : sword_skill
    {
    }
}