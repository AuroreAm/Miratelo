using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class SS1_Combo : pix
    {
        static readonly term[] SlashKeys = { AnimationKey.slash_0, AnimationKey.slash_1, AnimationKey.slash_2 };

        public motor[] Combo;

        public override void Create ()
        {
            Combo = new motor[3];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = new ac_slash ( SlashKeys[i] );
                b.IntegratePix (motor_slash);
                Combo[i] = motor_slash;
            }
        }
    }

    [Category("sword skill")]
    public class SS1 : skill_motor.First
    {
        [Depend]
        s_equip se;
        
        [Depend]
        SS1_Combo Combo;

        public override bool SkillValid => se.weaponUser is s_sword_user;


        public bool Spam (int ComboId)
        {
            return StartMotor ( Combo.Combo[ComboId] );
        }
    }

    [Category("sword skill")]
    public class SS2 : skill_motor.First
    {
        int ComboPtr;
        bool ReadyForCombo;

        [Depend]
        s_equip se;
        [Depend]
        SS1_Combo Combo;

        public override bool SkillValid => se.weaponUser is s_sword_user;

        public override void OnMotorEnd(motor m)
        {
            if (ReadyForCombo)
            ComboPtr ++;
            else
            {
                ComboPtr = 0;
                return;
            }

            ReadyForCombo = false;
            if (ComboPtr < Combo.Combo.Length)
            StartMotor ( Combo.Combo[ComboPtr] );
            else
            ComboPtr = 0;
        }

        public bool Spam ()
        {
            if ( !SkillActive )
            return StartMotor ( Combo.Combo[ComboPtr] );
            else if (!ReadyForCombo)
            {
                ReadyForCombo = true;
                return true;
            }
            return false;
        }
    }
}