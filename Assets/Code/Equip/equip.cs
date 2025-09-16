using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class equip : controller
    {
        public weapon_user weapon_user { private set; get; }
        public inventory inventory { private set; get; }

        protected override void _ready()
        {
            phoenix.core.execute (this);
        }

        public void link_weapon_user ( weapon_user _weapon_user )
        {
            if (weapon_user != null)
            Debug.LogError ("there's still weapon user active but changed it, a bug will occur since the previous weapon user was not free");

            weapon_user = _weapon_user;
            this.link ( _weapon_user );
        }

        public void remove_weapon_user ()
        {
            if (weapon_user == null)
            Debug.LogError ("Trying to remove unexisting weapon user");

            this.unlink ( weapon_user );
            weapon_user = null;
        }

        public void link_inventory ( inventory inventory )
        {
            this.inventory = inventory;
        }
    }
}
