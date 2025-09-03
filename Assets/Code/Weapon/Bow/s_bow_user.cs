using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_bow_user : s_weapon_user_standard <d_bow>
    {
        protected override int HandIndex => 0;
        protected override Quaternion DefaultRotation => Const.BowDefaultRotation;
        protected override int AniLayer => Skin.bow;
    }
}