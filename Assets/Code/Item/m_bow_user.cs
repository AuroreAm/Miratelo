using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_bow_user : m_weapon_user_standard<Bow>
    {
        public override SuperKey key => new SuperKey ("mbu");
        protected override int HandIndex => 0;
        protected override Quaternion DefaultRotation => Const.BowDefaultRotation;
        protected override int AniLayer => ms.bow;

        // rotation aimed by the target user
        public Vector3 rotY;

        public override void Main()
        {
        }
    }
}