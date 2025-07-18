using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class s_sword_user : s_weapon_user_standard<Sword>
    {
        public override term key => new term ("ssu");
        protected override int HandIndex => 1;
        protected override int AniLayer => ss.sword;
        protected override Quaternion DefaultRotation => Const.SwordDefaultRotation;

        // hardcoded slash animation for now
        public static readonly term[] SlashKeys = { AnimationKey.slash_0, AnimationKey.slash_1, AnimationKey.slash_2, AnimationKey.slash_3a };

        
        public static readonly term[] TaskIDS = new term[] { new term ( "SS2_0" ), new term ( "SS2_1" ), new term ( "SS2_2" ), new term ( "SS2_3a" ) };

        public static readonly term[] ParryKeys = { AnimationKey.parry_1 , AnimationKey.parry_2, AnimationKey.parry_1, AnimationKey.parry_2 };
    }
}
