using System;
using Pixify.Spirit;
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

    public class s_inv_0 : d_inv
    {
        [Depend]
        d_actor da;

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

        public class package : PreBlock.Package <s_inv_0>
        {
            public package (Transform[] SwordPlaces, Transform[] BowPlaces)
            {
                o.SwordPlaces = new WeaponPlace[SwordPlaces.Length];
                for (int i = 0; i < SwordPlaces.Length; i++)
                    o.SwordPlaces[i] = new WeaponPlace(SwordPlaces[i]);

                o.BowPlaces = new WeaponPlace[BowPlaces.Length];
                for (int i = 0; i < BowPlaces.Length; i++)
                    o.BowPlaces[i] = new WeaponPlace(BowPlaces[i]);
            }
        }

        public override void RegisterWeapon(Weapon weapon)
        {
            if (!FreePlaceExistFor(weapon))
                return;

            weapon.Aquire(da);
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

    /* INPROGRESS
    public class ac_set_adw_from_inv0 : action
    {
        [Depend]
        s_inv_0 si;

        public WeaponType WeaponType;
        public int index;

        protected override void Step()
        {
            WeaponPlace wp;

            switch (WeaponType)
            {
                case WeaponType.Sword: wp = si.SwordPlaces [index]; break;
                case WeaponType.Bow: wp = si.BowPlaces [index]; break;
            }

            si.character.GetAction<ac_draw_weapon>().SetPlaceToDrawFrom(wp);
            return true;
        }
    }

    
    public class ac_set_arw_from_me_to_inv0 : action
    {
        [Depend]
        s_equip se;
        [Depend]
        s_inv_0 si;

        protected override bool Step()
        {
            se.character.GetAction <ac_return_weapon> ().SetPlaceToReturn (  si.GetFreePlaceFor( se.weaponUser.WeaponBase ) );
            return true;
        }
    }*/
}