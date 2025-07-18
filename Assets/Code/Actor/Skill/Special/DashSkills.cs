using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("dash skill")]
    public class DS0_dash : skill_data
    {
        public override bool SkillCondition()
        {
            return b.HavePix <s_capsule_character_controller> ();
        }
    }
}
