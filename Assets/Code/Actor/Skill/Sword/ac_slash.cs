using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_SS2 : action
    {
        [Depend]
        mc_motor mm;
        int ComboPtr;
        bool ReadyForCombo;

        motor [] Combo;

        protected override void BeginStep()
        {
            ComboPtr = 0;
            ReadyForCombo = false;

            Combo = new motor[3];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = New <ac_slash> ( character );
                motor_slash.ComboId = i;
                Combo[i] = motor_slash;
            }

            StartSlash ();
        }

        void StartSlash ()
        {
            var Success = mm.SetState ( Combo[ComboPtr], SlashEnd );

            if (!Success)
            AppendStop ();
        }

        void SlashEnd ()
        {
            if (ReadyForCombo)
            ComboPtr ++;
            else
            {
                AppendStop ();
                return;
            }

            ReadyForCombo = false;
            if (ComboPtr < Combo.Length)
            StartSlash ();
            else
            AppendStop ();
        }

        public class ac_SS2_next_combo : action
        {
            [Depend]
            ac_SS2 ss2;

            protected override bool Step()
            {
                ss2.ReadyForCombo = true;
                return true;
            }
        }
    }

    public class ac_slash : motor
    {
        public override int Priority => Pri.Action;

        [Depend]
        m_sword_user msu;
        [Depend]
        m_skin ms;

        [Export]
        public int ComboId = 0;

        protected override void BeginStep()
        {
            BeginSlash(ComboId);
        }

        void BeginSlash ( int id )
        {
            ms.PlayState (0, m_sword_user.SlashKeys[id], 0.1f, EndSlash, null, Slash);
        }

        void Slash ()
        {
            p_slash_attack.Fire ( new SuperKey ( msu.Weapon.SlashName ), msu.Weapon, ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [1] - ms.EventPointsOfState ( m_sword_user.SlashKeys[ComboId] ) [0] );
        }

        void EndSlash ()
        {
            AppendStop();
        }
    }
}