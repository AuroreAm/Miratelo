using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class pr_sword : skill_reflexion <SS2>
    {
        protected override void SkillReflex ( SS2 Skill )
        {
            if ( Player.Action2.OnActive )
           Skill.Spam ();
        }
    }

    public class pr_sword_target : reflexion
    {
        [Depend]
        d_actor da;
        [Depend]
        s_equip se;

        [Depend]
        ac_lock_target alt;
        [Depend]
        pc_lateral_move plm;
        [Depend]
        pm_camera_target_target pmctt;
        
        public override void Create()
        {
            Link (alt);
            Link (plm);
            Link (pmctt);
        }

        protected override void Reflex()
        {
            if ( da.target && se.weaponUser is s_sword_user )
            Stage.Start (this);
        }

        
        protected override void Step()
        {
            if (!( da.target && se.weaponUser is s_sword_user ))
            SelfStop ();
        }
    }
}