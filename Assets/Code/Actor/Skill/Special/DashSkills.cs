using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("dash skill")]
    public class DS0_dash : skill_data
    {
        [Depend]
        m_actor ma;

        public override bool SkillCondition()
        {
            return ma.character.HaveModule <m_capsule_character_controller> ();
        }
    }
}
