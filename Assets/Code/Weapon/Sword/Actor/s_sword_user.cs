using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public class s_sword_user : s_weapon_user_standard<Sword>
    {
        protected override int HandIndex => 1;
        protected override int AniLayer => ss.sword;
        protected override Quaternion DefaultRotation => Const.SwordDefaultRotation;
    }
}
