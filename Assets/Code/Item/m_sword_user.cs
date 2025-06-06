using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_sword_user : m_weapon_user_standard<Sword>
    {
        protected override int HandIndex => 1;
        protected override int AniLayer => ms.sword;
        protected override Quaternion DefaultRotation => Const.SwordDefaultRotation;

        public override void Main()
        {
        }
    }
}
