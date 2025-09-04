using System;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class WeaponPlace
    {
        d_weapon_standard _weapon;
        Transform place;
        public bool Occupied => _weapon != null;

        public WeaponPlace(Transform Place)
        {
            place = Place;
        }

        public void Put( d_weapon_standard weapon )
        {
            if ( _weapon != null )
                Debug.LogError("place already occupied");

            _weapon = weapon;
            _weapon.Coord.SetParent(place);
            _weapon.Coord.localPosition = Vector3.zero;
            _weapon.Coord.localRotation = Quaternion.identity;
        }

        public d_weapon_standard Get() => _weapon;

        public d_weapon_standard Free()
        {
            if ( _weapon == null )
            throw new InvalidOperationException("place not occupied");

            d_weapon_standard w = _weapon;
            w.Coord.SetParent(null);
            _weapon = null;
            return w;
        }
    }

    [inkedAttribute]
    public class d_inv_0 : d_inventory
    {
        [harmony]
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

        public class package : ink <d_inv_0>
        {
            public package (Transform[] swordplaces, Transform[] bowplaces)
            {
                o.SwordPlaces = new WeaponPlace[swordplaces.Length];
                for (int i = 0; i < swordplaces.Length; i++)
                    o.SwordPlaces[i] = new WeaponPlace(swordplaces[i]);

                o.BowPlaces = new WeaponPlace[bowplaces.Length];
                for (int i = 0; i < bowplaces.Length; i++)
                    o.BowPlaces[i] = new WeaponPlace(bowplaces[i]);
            }
        }

        public override void RegisterWeapon(d_weapon_standard weapon)
        {
            if (!FreePlaceExistFor(weapon))
                return;

            weapon.Aquire(da);
            GetFreePlaceFor(weapon).Put(weapon);
        }

        public bool FreePlaceExistFor(d_weapon_standard weapon)
        {
            if (weapon is d_sword)
                for (int i = 0; i < SwordPlaces.Length; i++)
                {
                    if (!SwordPlaces[i].Occupied)
                        return true;
                }

            if (weapon is d_bow)
                for (int i = 0; i < BowPlaces.Length; i++)
                {
                    if (!BowPlaces[i].Occupied)
                        return true;
                }
            return false;
        }

        public WeaponPlace GetFreePlaceFor(d_weapon_standard weapon)
        {
            if (weapon is d_sword)
                return GetFreeSwordPlace();
            else if (weapon is d_bow)
                return GetFreeBowPlace();
            return null;
        }
    }
}