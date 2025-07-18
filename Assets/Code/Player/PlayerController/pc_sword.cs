using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{//INPROGRESS
    /*
    // do lateral move and focus the camera when targetting
    public class pr_sword_target : reflection
    {
        [Depend]
        m_actor ma;
        [Depend]
        m_equip me;
        [Depend]
        pm_camera_target_target pmctt;
        
        action LockTargetSword;

        override public void Create()
        {
            TreeStart ( me.character );
            new parallel () {StopWhenFirstNodeStopped = true};
                new ac_have_target ();
                new pc_lateral_move ();
                new ac_lock_target ();
            end();
            LockTargetSword = TreeFinalize ();
        }

        public override void Main()
        {
            if ( ma.target && me.weaponUser is m_sword_user && mm.priority < Pri.def2nd)
                mm.SetState ( LockTargetSword, Pri.def2nd );

            if ( me.weaponUser is m_sword_user && ma.target && !pmctt.aquired )
                pmctt.Aquire (this);

            if ( me.weaponUser is m_sword_user && !ma.target && pmctt.aquired )
                pmctt.Free (this);
        }
    }

    public class pr_slash_consecutive : reflection
    {

        [Depend]
        m_equip me;

        controlled_sequence slash_combo;

        public override void Create()
        {
            TreeStart ( me.character );
            new controlled_sequence () { repeat = false };
                new ac_slash () { ComboId = 0 };
                new ac_slash () { ComboId = 1 };
                new ac_slash () { ComboId = 2 };
            end ();
            slash_combo = TreeFinalize () as controlled_sequence;
        }

        override public void Main()
        {
            if (Player.Action2.OnActive)
            {
                if ( mm.state != slash_combo )
                    mm.SetState (slash_combo, Pri.Action);
                else
                {
                    slash_combo.TaskStatus = controlled_sequence.TaskStatusEnum.Success;
                }
            }
        }
    }*/

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