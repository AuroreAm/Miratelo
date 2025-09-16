using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class Inv0Author : ActorAuthorModule
    {
        public List <WeaponAuthor> AttachedWeapon;


        public override void _create()
        {}

        public override void _created (system system)
        {
            for (int i = 0; i < AttachedWeapon.Count; i++)
            system.get <equip> ().inventory.register_weapon ( AttachedWeapon[i].create_instance () );
        }
    }
}