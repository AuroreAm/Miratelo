using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class s_weapon_user : controller
    {
        public abstract term key {get;}
        public abstract Weapon WeaponBase {get;}
        public abstract void SetWeaponBase ( Weapon weapon );
    }

    public abstract class s_weapon_user <T> : s_weapon_user where T : Weapon
    {
        [Depend]
        protected s_skin ss;

        public T Weapon { get; protected set; }
        public override Weapon WeaponBase => Weapon;

        public override void SetWeaponBase(Weapon weapon)
        {
            Weapon = weapon as T;
        }
    }

    public abstract class s_weapon_user_standard <T> : s_weapon_user <T> where T:Weapon
    {
        [Depend]
        protected d_hand dh;

        protected abstract int HandIndex { get; }
        protected abstract int AniLayer { get; }
        protected abstract Quaternion DefaultRotation { get; }

        protected sealed override void Start ()
        {
            if (Weapon)
            {
                Weapon.transform.SetParent( dh.Hand[HandIndex] );
                Weapon.transform.localPosition = Vector3.zero;
                Weapon.transform.localRotation = DefaultRotation;
                // TODO Add user actor
                ss.ActivateSyncLayer(AniLayer);
                Start1();
            }
            else
                Debug.LogError("Weapon user activated but no weapon attached to it");
        }

        protected sealed override void Stop()
        {
            if (Weapon)
            {
                Weapon.transform.SetParent(null);
                // TODO remove user actor
                Weapon = null;
                ss.DisableSyncLayer(AniLayer);
            }
        }

        protected virtual void Start1 ()
        { }
    }
}
