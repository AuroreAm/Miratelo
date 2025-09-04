using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_equip : will
    {
        public s_weapon_user WeaponUser { private set; get; }
        public d_inventory Inventory { private set; get; }

        protected override void harmony()
        {
            phoenix.core.start (this);
        }

        public void SetWeaponUser ( s_weapon_user swu )
        {
            if (WeaponUser != null)
            Debug.LogError ("there's still weapon user active but changed it, a bug will occur since the previous weapon user was not free");

            WeaponUser = swu;
            this.link ( swu );
        }

        public void RemoveWeaponUser ()
        {
            if (WeaponUser == null)
            Debug.LogError ("Trying to remove unexisting weapon user");

            this.unlink ( WeaponUser );
            WeaponUser = null;
        }

        public void SetInventory ( d_inventory inventory )
        {
            Inventory = inventory;
        }
    }
}
