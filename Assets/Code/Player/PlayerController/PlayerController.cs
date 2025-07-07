using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    
    [Category("player controller")]
    public class player_cortex : cortex
    {
        [Depend]
        m_skill ms;

        protected override void Think()
        {
            // basic movement
            AddReflection <pr_move> ();
            AddReflection <pr_jump> ();
            AddReflection <r_fall_with_hard> ();

            // basic commands
            AddReflection <pr_equip> ();
            AddReflection <pr_target> ();
            AddReflection <pr_interact_near_weapon> ();

            // dash skill
            if ( ms.SkillValid <DS0_dash> () )
            AddReflection <pr_dash> ();
            
            // Sword skill
            if ( ms.SkillValid <SS2_consecutive> () )
            {
                AddReflection < pr_slash_consecutive > ();
                AddMaster (ms.GetSkill <SS2_consecutive> ().slash_0);
                AddMaster (ms.GetSkill <SS2_consecutive> ().slash_1);
                AddMaster (ms.GetSkill <SS2_consecutive> ().slash_2);
            }
        }
    }
}