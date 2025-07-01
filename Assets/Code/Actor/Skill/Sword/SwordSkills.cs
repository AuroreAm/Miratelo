using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("sword skill")]
    public abstract class sword_skill : skill_data
    {
        [Depend]
        m_equip me;

        public override bool SkillCondition()
        {
            return me.weaponUser is m_sword_user ;
        }
    }

    public class SS2_consecutive : sword_skill
    {
    }

    public class SS3_hooker_up : sword_skill
    {
    }

    public class SS4_knocker : sword_skill
    {
    }
}