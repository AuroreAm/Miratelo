using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class inventory : moon
    {
        [link]
        equip equip;

        protected override void _ready()
        {
            equip.link_inventory ( this );
        }

        public virtual void try_put_weapon ( weapon weapon )
        {}
    }
}
