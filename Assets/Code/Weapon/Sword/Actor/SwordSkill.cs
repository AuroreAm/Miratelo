using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class SS1_Combo : pix
    {
        static readonly term[] SlashKeys = { AnimationKey.SS1_0, AnimationKey.SS1_1, AnimationKey.SS1_2 };

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
            if ( !Active )
            return StartMotor ( Combo.Combo[ComboPtr] );
            else if (!ReadyForCombo)
            {
                ReadyForCombo = true;
                return true;
            }
            return false;
        }
    }

    [ Category ( "sword skill" ) ]
    public class SS8_parry : skill_motor.First
    {
        [Depend]
        s_equip _equip;

        public override bool SkillValid => _equip.weaponUser is s_sword_user;

        Dictionary < term, motor > _parryKeys;

        public override void Create()
        {
            _parryKeys = new Dictionary<term, motor> ();

            var parry0 = new ac_parry ( AnimationKey.SS8_0 );
            var parry1 = new ac_parry ( AnimationKey.SS8_1 );
            b.IntegratePix ( parry0 );
            b.IntegratePix ( parry1 );

            _parryKeys.Add ( AnimationKey.SS1_0, parry1 );
            _parryKeys.Add ( AnimationKey.SS1_1, parry0 );
            _parryKeys.Add ( AnimationKey.SS1_2, parry1 );

            _parryKeys.Add ( AnimationKey.SS4, parry1 );
        }

        public bool Spam ( term incomming_slash_animation )
        {
            if ( _parryKeys.ContainsKey ( incomming_slash_animation ) )
            return StartMotor ( _parryKeys [incomming_slash_animation] );
            else
            return StartMotor (_parryKeys [AnimationKey.SS1_0]);
        }
    }
}