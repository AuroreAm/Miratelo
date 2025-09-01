using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ar_sword : skill_reflexion <SS1>
    {
        [Depend]
        t_SS1 t_SS1;

        protected override void SkillReflex(SS1 skill)
        {
            if (t_SS1.on)
            {
                if (skill.Spam ( t_SS1.ComboId ))
                Stage.Start (this);
            }
        }

        protected override void Step()
        {
            if (!skill.Active)
            t_SS1.Finish ();
        }
    }

    [Category ("actor")]
    public class slash_condition : condition
    {
        [Depend]
        s_equip se;

        public override bool Check()
        {
            return se.weaponUser is s_sword_user;
        }

        public override term solution => Commands.draw_sword;
    }

}
