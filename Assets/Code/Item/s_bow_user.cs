using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_bow_user : s_weapon_user_standard<Bow>
    {
        public override term key => new term ("sbu");
        protected override int HandIndex => 0;
        protected override Quaternion DefaultRotation => Const.BowDefaultRotation;
        protected override int AniLayer => ss.bow;

        // rotation aimed by the target user
        public Vector3 rotY;
    }
}