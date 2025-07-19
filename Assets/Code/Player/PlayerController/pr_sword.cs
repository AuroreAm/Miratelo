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

    public class pr_sword : reflexion
    {
        [Depend]
        s_mind sm;

        [Depend]
        pc_SS2 pc_SS2;

        protected override void Step()
        {
            if ( !Player.Action2.OnActive )
            return;

            if ( !pc_SS2.on )
            {
                Stage.Start ( pc_SS2 );
                return;
            }
            else
                pc_SS2.PrepareNextCombo ();
        }
    }

    public class pc_SS2 : action, IMotorHandler
    {
        [Depend]
        s_motor sm;
        motor [] Combo;

        int ComboPtr;
        bool ReadyForCombo;

        public override void Create()
        {
            Combo = new motor[3];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = new ac_slash (i);
                b.IntegratePix (motor_slash);
                Combo[i] = motor_slash;
            }
        }

        protected override void Start()
        {
            ComboPtr = 0;
            ReadyForCombo = false;

            StartSlash ();
        }

        void StartSlash ()
        {
            var Success = sm.SetState ( Combo[ComboPtr], this );

            if (!Success)
            SelfStop ();
        }

        public void PrepareNextCombo ()
        {
            ReadyForCombo = true;
        }

        public void OnMotorEnd(motor m)
        {
            if (!on)
            return;

            if (ReadyForCombo)
            ComboPtr ++;
            else
            {
                SelfStop ();
                return;
            }

            ReadyForCombo = false;
            if (ComboPtr < Combo.Length)
            StartSlash ();
            else
            SelfStop ();
        }
    }

}