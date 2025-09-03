using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class s_weapon_user : controller
    {
        public abstract d_weapon_standard WeaponBase {get;}
        public abstract void SetWeaponBase ( d_weapon_standard weapon );
    }

    public abstract class s_weapon_user <T> : s_weapon_user where T : d_weapon_standard
    {
        [Link]
        protected s_skin Skin;

        public T Weapon { get; protected set; }
        public override d_weapon_standard WeaponBase => Weapon;

        public override void SetWeaponBase(d_weapon_standard weapon)
        {
            Weapon = weapon as T;
        }
    }

    public abstract class s_weapon_user_standard <T> : s_weapon_user <T> where T : d_weapon_standard
    {
        [Link]
        protected d_hand Hands;

        protected abstract int HandIndex { get; }
        protected abstract int AniLayer { get; }
        protected abstract Quaternion DefaultRotation { get; }

        protected sealed override void OnStart ()
        {
            if ( Weapon != null )
            {
                Weapon.Coord.SetParent( Hands.Hands[HandIndex] );
                Weapon.Coord.localPosition = Vector3.zero;
                Weapon.Coord.localRotation = DefaultRotation;
                // TODO Add user actor
                Skin.ActivateSyncLayer(AniLayer);
                OnStart1 ();
            }
            else
                Debug.LogError("Weapon user activated but no weapon attached to it");
        }

        protected sealed override void OnStop()
        {
            if ( Weapon != null )
            {
                Weapon.Coord.SetParent(null);
                // TODO remove user actor
                Weapon = null;
                Skin.DisableSyncLayer(AniLayer);
            }
        }

        protected virtual void OnStart1 ()
        { }
    }
}
