using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class weapon_user : controller
    {
        public abstract weapon weapon_base {get;}
        public abstract void SetWeaponBase ( weapon weapon );
    }

    public abstract class weapon_user <T> : weapon_user where T : weapon
    {
        [link]
        protected skin skin;

        public T weapon { get; protected set; }
        public override weapon weapon_base => weapon;

        public override void SetWeaponBase(weapon _weapon)
        {
            weapon = _weapon as T;
        }
    }

    public abstract class weapon_user_standard <T> : weapon_user <T> where T : weapon
    {
        [link]
        protected hands hands;

        protected abstract int hand { get; }
        protected abstract int layer { get; }
        protected abstract Quaternion default_rotation { get; }

        protected sealed override void _start ()
        {
            if ( weapon != null )
            {
                var hands = ( Transform [] ) this.hands;
                weapon.coord.SetParent( hands [hand] );
                weapon.coord.localPosition = Vector3.zero;
                weapon.coord.localRotation = default_rotation;
                // TODO Add user actor
                skin.enable_sync_layer(layer);
                __start ();
            }
            else
                Debug.LogError("Weapon user activated but no weapon attached to it");
        }

        protected sealed override void _stop()
        {
            if ( weapon != null )
            {
                weapon.coord.SetParent(null);
                // TODO remove user actor
                weapon = null;
                skin.disable_sync_layer(layer);
            }
        }

        protected virtual void __start ()
        { }
    }
}
