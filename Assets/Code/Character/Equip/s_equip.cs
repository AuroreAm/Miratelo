using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_equip : pix
    {
        int swu_key;
        public s_weapon_user weaponUser {private set; get;}
        public d_inv inventory { private set; get; }

        public void SetWeaponUser ( s_weapon_user swu )
        {
            if (weaponUser != null)
            Debug.LogError ("there's still weapon user active but changed it, a bug will occur since the previous weapon user was not free");

            weaponUser = swu;
            swu_key = Stage.Start ( swu );
        }

        public void RemoveWeaponUser ()
        {
            if (weaponUser == null)
            Debug.LogError ("Trying to remove unexisting weapon user");

            Stage.Stop (swu_key);
            weaponUser = null;
        }

        public void Link ( d_inv inventory )
        {
            this.inventory = inventory;
        }
    }
}
