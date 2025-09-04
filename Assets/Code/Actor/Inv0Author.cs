using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class Inv0Author : ActorAuthorModule
    {
        public List <SwordAuthor> AttachedWeapon;

        public override void OnStructure()
        {}

        public override void OnStructureReady(shard.constelation structure)
        {
            for (int i = 0; i < AttachedWeapon.Count; i++)
            structure.get <s_equip> ().Inventory.RegisterWeapon( AttachedWeapon[i].Get () );
        }
    }
}