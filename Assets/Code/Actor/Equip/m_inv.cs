using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class m_inv : module
    {
        [Depend]
        m_equip me;

        public sealed override void Create()
        {
            me.Link (this);
        }
        public abstract void RegisterWeapon ( Weapon weapon );
    }
}