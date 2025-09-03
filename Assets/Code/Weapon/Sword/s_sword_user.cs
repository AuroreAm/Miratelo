using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_sword_user : s_weapon_user_standard <d_sword>
    {
        protected override int HandIndex => 1;
        protected override int AniLayer => Skin.sword;
        protected override Quaternion DefaultRotation => Const.SwordDefaultRotation;
    }
}
