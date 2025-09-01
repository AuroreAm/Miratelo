using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using Lyra.Spirit;

namespace Triheroes.Code
{
    public class pr_SS2 : skill_reflexion <SS2>
    {
        protected override void SkillReflex ( SS2 Skill )
        {
            if ( Player.Action2.OnActive )
           Skill.Spam ();
        }
    }

    public class pr_parry : reflexion
    {
        [Depend]
        d_skill ds;

        [Depend]
        r_slash_alert _slashAlert;

        SS8_parry SS8 => ds.GetSkill < SS8_parry > ();

        protected override void Reflex()
        {
            if ( ds.SkillValid <SS8_parry> () && Player.Action3.OnActive )
            SS8.Spam ( _slashAlert.IncommingSlash.Slash );
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