using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class d_inv : pix
    {
        [Depend]
        s_equip se;

        public sealed override void Create()
        {
            se.Link (this);
        }
        public abstract void RegisterWeapon ( Weapon weapon );
    }
}