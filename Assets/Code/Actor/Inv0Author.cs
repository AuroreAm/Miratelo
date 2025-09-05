using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class Inv0Author : ActorAuthorModule
    {
        public List <SwordAuthor> AttachedWeapon;

        public override void _creation()
        {}

        public override void _creation (system system)
        {
            for (int i = 0; i < AttachedWeapon.Count; i++)
            system.get <equip> ().inventory.register_weapon( AttachedWeapon[i].Get () );
        }
    }
}