using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_inventory : dat
    {

        [Link]
        s_equip equip;

        protected override void OnStructured()
        {
            equip.SetInventory ( this );
        }

        public virtual void RegisterWeapon ( d_weapon_standard weapon )
        {}

    }
}
