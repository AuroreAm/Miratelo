using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_equip : module
    {
        public m_weapon_user weaponUser;
        public m_inv inventory { private set; get; }

        public void Link ( m_inv inventory )
        {
            this.inventory = inventory;
        }
    }
}
