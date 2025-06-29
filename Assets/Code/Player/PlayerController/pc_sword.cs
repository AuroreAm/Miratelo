using System.Collections;
using System.Collections.Generic;
using Pixify;
using static Pixify.treeBuilder;

namespace Triheroes.Code
{
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
            if ( ma.target && me.weaponUser is m_sword_user && mst.priority < Pri.def2nd)
                mst.SetState ( LockTargetSword, Pri.def2nd );

            if ( me.weaponUser is m_sword_user && ma.target && !pmctt.aquired )
                pmctt.Aquire (this);

            if ( me.weaponUser is m_sword_user && !ma.target && pmctt.aquired )
                pmctt.Free (this);
        }
    }

    public class pr_slash : reflection
    {
        [Depend]
        m_equip me;

        [Depend]
        pr_sword_target pst;

        controlled_sequence slash_combo;
        controlled_sequence slash_combo2;

        public override void Create()
        {
            TreeStart ( me.character );
            new controlled_sequence () { repeat = false };
                new ac_slash () { ComboId = 0 };
                new ac_slash () { ComboId = 1 };
                new ac_slash () { ComboId = 2 };
                new parallel () {StopWhenFirstNodeStopped = true };
                    slash_combo2 = new controlled_sequence () { repeat = false };
                        new ac_slash_knocked_forced ();
                        new ac_slash () { ComboId = 0 };
                        new ac_slash () { ComboId = 1 };
                        new ac_slash () { ComboId = 2 };
                    end ();
                    new ac_active_knock_forcer ();
                end ();
            end();
            slash_combo = TreeFinalize () as controlled_sequence;
        }

        override public void Main()
        {
            if (Player.Action2.OnActive && me.weaponUser is m_sword_user)
            {
                if ( mst.state != slash_combo )
                    mst.SetState (slash_combo, Pri.Action);
                else
                {
                    slash_combo.TaskStatus = controlled_sequence.TaskStatusEnum.Success;
                    slash_combo2.TaskStatus = controlled_sequence.TaskStatusEnum.Success;
                }
            }
        }
    }
}