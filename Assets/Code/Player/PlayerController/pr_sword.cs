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
            Stage.Start1 (pst);
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

    public class pr_sword : reflexion, IMotorHandler, IElementListener <incomming_slash>
    {
        [Depend]
        s_motor sm;

        [Depend]
        pc_SS2 pc_SS2;

        [Depend]
        ac_parry ac_parry;

        [Depend]
        s_element se;

        public void OnMotorEnd(motor m)
        {}

        protected override void Start()
        {
            se.LinkMessage (this);
        }

        protected override void Stop()
        {
            se.UnlinkMessage (this);
        }

        protected override void Step()
        {
            SS2_skill ();
            SS3_skill ();
        }

        void SS2_skill ()
        {
            if ( !Player.Action2.OnActive )
            return;

            if ( !pc_SS2.on )
            {
                Stage.Start1 ( pc_SS2 );
                return;
            }
            else
                pc_SS2.PrepareNextCombo ();
        }

        void SS3_skill ()
        {
            if ( !Player.Alt.OnActive )
            return;

            if ( !ac_parry.on )
                sm.SetState ( ac_parry, this );
        }

        public void OnMessage(incomming_slash context)
        {
            ac_parry.OverrideAnimation ( SS3_parry.ParryKeys [context.slash] );
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
            Combo = new motor[4];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = new ac_slash ( SS2.SlashKeys [i] );
                b.IntegratePix (motor_slash);
                Combo[i] = motor_slash;
            }

            Combo [3] = new ac_slash_hooker_up ( SS2.SlashKeys [1] );
            b.IntegratePix (Combo [3]);
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