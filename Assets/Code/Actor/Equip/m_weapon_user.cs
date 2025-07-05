using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class m_weapon_user : core
    {
        public abstract SuperKey key {get;}
        public abstract Weapon WeaponBase {get;}
        public abstract void SetWeaponBase ( Weapon weapon );
    }

    public abstract class m_weapon_user <T> : m_weapon_user where T : Weapon
    {
        [Depend]
        protected m_skin ms;

        public T Weapon { get; protected set; }
        public override Weapon WeaponBase => Weapon;

        public override void SetWeaponBase(Weapon weapon)
        {
            Weapon = weapon as T;
        }
    }

    public abstract class m_weapon_user_standard <T> : m_weapon_user <T> where T:Weapon
    {
        [Depend]
        protected m_hand mh;

        protected abstract int HandIndex { get; }
        protected abstract int AniLayer { get; }
        protected abstract Quaternion DefaultRotation { get; }

        protected sealed override void OnAquire()
        {
            if (Weapon)
            {
                Weapon.transform.SetParent( mh.Hand[HandIndex] );
                Weapon.transform.localPosition = Vector3.zero;
                Weapon.transform.localRotation = DefaultRotation;
                // TODO Add user actor
                ms.ActivateSyncLayer(AniLayer);
                OnAquire1();
            }
            else
                Debug.LogError("Weapon user activated but no weapon attached to it");
        }

        protected sealed override void OnFree()
        {
            if (Weapon)
            {
                Weapon.transform.SetParent(null);
                // TODO remove user actor
                Weapon = null;
                ms.DisableSyncLayer(AniLayer);
            }
        }

        protected virtual void OnAquire1()
        { }

        protected virtual void Create1()
        { }
    }
}
