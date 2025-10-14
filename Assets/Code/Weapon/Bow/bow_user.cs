using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class bow_user : weapon_user_standard <bow>
    {
        protected override int hand => 0;
        protected override Quaternion default_rotation => val.def_bow_rotation;
        protected override int layer => skin.bow;
    }
}