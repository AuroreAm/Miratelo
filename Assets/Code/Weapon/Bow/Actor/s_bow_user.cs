using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_bow_user : s_weapon_user_standard<Bow>
    {
        protected override int HandIndex => 0;
        protected override Quaternion DefaultRotation => Const.BowDefaultRotation;
        protected override int AniLayer => ss.bow;
    }
}