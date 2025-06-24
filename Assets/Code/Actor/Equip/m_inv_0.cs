using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class WeaponPlace
    {
        Weapon weapon;
        Transform place;
        public bool Occupied => weapon;

        public WeaponPlace(Transform Place)
        {
            place = Place;
        }

        public void Put(Weapon Weapon)
        {
            if (weapon)
                Debug.LogError("place already occupied");

            weapon = Weapon;
            weapon.transform.SetParent(place);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }

        public Weapon Get() => weapon;

        public Weapon Free()
        {
            if (!weapon)
            throw new InvalidOperationException("place not occupied");

            Weapon w = weapon;
            w.transform.SetParent(null);
            weapon = null;
            return w;
        }
    }

    public class m_inv_0 : m_inv
    {
        [Depend]
        m_actor ma;

        public WeaponPlace[] SwordPlaces { private set; get; }
        public WeaponPlace GetFreeSwordPlace ()
        {
            for (int i = 0; i < SwordPlaces.Length; i++)
            {
                if (!SwordPlaces[i].Occupied)
                    return SwordPlaces[i];
            }
            return null;
        }
        public WeaponPlace[] BowPlaces { private set; get; }
        public WeaponPlace GetFreeBowPlace ()
        {
            for (int i = 0; i < BowPlaces.Length; i++)
            {
                if (!BowPlaces[i].Occupied)
                    return BowPlaces[i];
            }
            return null;
        }

        public void SetPlaces(Transform[] SwordPlaces, Transform[] BowPlaces)
        {
            this.SwordPlaces = new WeaponPlace[SwordPlaces.Length];
            for (int i = 0; i < SwordPlaces.Length; i++)
                this.SwordPlaces[i] = new WeaponPlace(SwordPlaces[i]);

            this.BowPlaces = new WeaponPlace[BowPlaces.Length];
            for (int i = 0; i < BowPlaces.Length; i++)
                this.BowPlaces[i] = new WeaponPlace(BowPlaces[i]);
        }

        public override void RegisterWeapon(Weapon weapon)
        {
            if (!FreePlaceExistFor(weapon))
                return;

            weapon.Aquire(ma);
            GetFreePlaceFor(weapon).Put(weapon);
        }

        public bool FreePlaceExistFor(Weapon weapon)
        {
            if (weapon is Sword)
                for (int i = 0; i < SwordPlaces.Length; i++)
                {
                    if (!SwordPlaces[i].Occupied)
                        return true;
                }

            if (weapon is Bow)
                for (int i = 0; i < BowPlaces.Length; i++)
                {
                    if (!BowPlaces[i].Occupied)
                        return true;
                }
            return false;
        }

        public WeaponPlace GetFreePlaceFor(Weapon weapon)
        {
            if (weapon is Sword)
                return GetFreeSwordPlace();
            else if (weapon is Bow)
                return GetFreeBowPlace();
            return null;
        }
    }

    public class ac_set_adw_from_inv0 : action
    {
        [Depend]
        m_inv_0 mi;

        public WeaponType WeaponType;
        public int index;

        protected override bool Step()
        {
            WeaponPlace wp;

            switch (WeaponType)
            {
                case WeaponType.Sword: wp = mi.SwordPlaces [index]; break;
                case WeaponType.Bow: wp = mi.BowPlaces [index]; break;
                default: return false;
            }
            mi.character.GetUnique<ac_draw_weapon>().SetPlaceToDrawFrom(wp);
            return true;
        }
    }

    public class ac_set_arw_from_me_to_inv0 : action
    {
        [Depend]
        m_equip me;
        [Depend]
        m_inv_0 mi;

        protected override bool Step()
        {
            me.character.GetUnique <ac_return_weapon> ().SetPlaceToReturn (  mi.GetFreePlaceFor( me.weaponUser.WeaponBase ) );
            return true;
        }
    }
}