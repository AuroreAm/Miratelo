using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class ac_SS2 : action, IMotorHandler
    {
        [Depend]
        s_motor sm;
        int ComboPtr;
        bool ReadyForCombo;

        motor [] Combo;

        protected override void Start()
        {
            ComboPtr = 0;
            ReadyForCombo = false;

            Combo = new motor[3];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = new ac_slash (i);
                b.IntegratePix (motor_slash);
                Combo[i] = motor_slash;
            }

            StartSlash ();
        }

        void StartSlash ()
        {
            var Success = sm.SetState ( Combo[ComboPtr], this );

            if (!Success)
            SelfStop ();
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

        public class ac_SS2_next_combo : action
        {
            [Depend]
            ac_SS2 ss2;

            protected override void Start()
            {
                ss2.ReadyForCombo = true;
                SelfStop ();
            }
        }
    }

    public class ac_slash : motor
    {
        public override int Priority => Pri.Action;

        [Depend]
        s_sword_user ssu;
        [Depend]
        s_skin ss;

        int ComboId = 0;

        public ac_slash (int ComboID)
        {
            ComboId = ComboID;
        }

        protected override void Start()
        {
            BeginSlash(ComboId);
        }

        void BeginSlash ( int id )
        {
            ss.PlayState (0, s_sword_user.SlashKeys[id], 0.1f, EndSlash, null, Slash);
        }

        void Slash ()
        {
            a_slash_attack.Fire ( new term ( ssu.Weapon.SlashName ), ssu.Weapon, ss.EventPointsOfState ( s_sword_user.SlashKeys[ComboId] ) [1] - ss.EventPointsOfState ( s_sword_user.SlashKeys[ComboId] ) [0] );
        }

        void EndSlash ()
        {
            SelfStop();
        }
    }
}