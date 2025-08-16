using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class pr_sword : reflexion
    {
        [Depend]
        d_skill ds;

        protected override void Step()
        {
            SS2_skill ();
        }

        void SS2_skill ()
        {
            if ( ds.SkillValid <SS2> () && Player.Action2.OnActive  )
            ds.GetSkill <SS2> ().Spam ();
        }
    }

    public class pr_sword_target : reflexion
    {
        [Depend]
        d_actor da;
        [Depend]
        s_equip se;
        [Depend]
        pc_sword_target pst;

        protected override void Step()
        {
            if ( !pst.on && da.target && se.weaponUser is s_sword_user )
            Stage.Start (pst);
        }
    }

    public class pc_sword_target : action
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

        protected override void Step()
        {
            if (!( da.target && se.weaponUser is s_sword_user ))
            SelfStop ();
        }
    }

}