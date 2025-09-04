using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_inventory : shard
    {

        [harmony]
        s_equip equip;

        protected override void harmony()
        {
            equip.SetInventory ( this );
        }

        public virtual void RegisterWeapon ( d_weapon_standard weapon )
        {}

    }
}
