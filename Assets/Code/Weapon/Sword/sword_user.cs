using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class sword_user : weapon_user_standard <sword>
    {
        protected override int hand => 1;
        protected override int layer => skin.sword;
        protected override Quaternion default_rotation => Const.SwordDefaultRotation;
    }
}
