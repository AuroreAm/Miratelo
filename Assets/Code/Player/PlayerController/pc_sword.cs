using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
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

        int key_lt, key_ctt, key_lm;

        protected override void Start()
        {
            key_lt = Stage.Start (alt);
            key_ctt = Stage.Start ( pmctt );
            key_lm = Stage.Start ( plm );
        }

        protected override void Step()
        {
            if (!( da.target && se.weaponUser is s_sword_user ))
            SelfStop ();
        }

        protected override void Stop()
        {
            Stage.Stop ( key_lt );
            Stage.Stop ( key_ctt );
            Stage.Stop ( key_lm );
        }
    }

    public class pr_slash_consecutive : reflexion
    {
        [Depend]
        s_mind sm;

        [Depend]
        ac_SS2 ac_SS2;
        [Depend]
        ac_SS2.ac_SS2_next_combo ac_SS2_Next_Combo;

        protected override void Step()
        {
            if ( !Player.Action2.OnActive )
            return;

            if ( !ac_SS2.on )
            {
                Stage.Start ( ac_SS2 );
                return;
            }
            else if ( ! ac_SS2_Next_Combo.on )
            {
                Stage.Start ( ac_SS2_Next_Combo );
            }
        }
    }
}