using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_equip : module
    {
        public m_weapon_user weaponUser {private set; get;}
        public m_inv inventory { private set; get; }

        public void SetWeaponUser ( m_weapon_user mwu )
        {
            if (weaponUser != null)
            Debug.LogError ("there's still weapon user active but changed it, a bug will occur since the previous weapon user was not free");

            weaponUser = mwu;
            weaponUser.Aquire (this);
        }

        public void RemoveWeaponUser ()
        {
            if (weaponUser == null)
            Debug.LogError ("Trying to remove unexisting weapon user");

            weaponUser.Free (this);
            weaponUser = null;
        }

        public void Link ( m_inv inventory )
        {
            this.inventory = inventory;
        }
    }
}
