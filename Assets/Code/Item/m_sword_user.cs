using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class m_sword_user : m_weapon_user_standard<Sword>
    {
        protected override int HandIndex => 1;
        protected override int AniLayer => ms.sword;
        protected override Quaternion DefaultRotation => Const.SwordDefaultRotation;

        public int state = StateKey.zero;

        // hardcoded slash animation for now
        public static readonly SuperKey[] SlashKeys = { AnimationKey.slash_0, AnimationKey.slash_1, AnimationKey.slash_2, AnimationKey.slash_3a };
        public static readonly SuperKey[] ParryKeys = { AnimationKey.parry_1 , AnimationKey.parry_2, AnimationKey.parry_1, AnimationKey.parry_2 };


        public override void Main()
        {
        }
    }
}
