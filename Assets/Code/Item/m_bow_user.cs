using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_bow_user : m_weapon_user_standard<Bow>
    {
        protected override int HandIndex => 0;
        protected override Quaternion DefaultRotation => Const.BowDefaultRotation;
        protected override int AniLayer => ms.bow;

        public override void Main()
        {
        }
    }
}
