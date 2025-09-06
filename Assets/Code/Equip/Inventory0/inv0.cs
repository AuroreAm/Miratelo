using System;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class weapon_place
    {
        weapon weapon;
        Transform place;
        public bool occupied => weapon != null;

        public weapon_place (Transform _place)
        {
            place = _place;
        }

        public void put ( weapon _weapon )
        {
            if ( weapon != null )
                Debug.LogError("place already occupied");

            weapon = _weapon;
            weapon.coord.SetParent(place);
            weapon.coord.localPosition = Vector3.zero;
            weapon.coord.localRotation = Quaternion.identity;
        }

        public weapon get () => weapon;

        public weapon free ()
        {
            if ( weapon == null )
            throw new InvalidOperationException("place not occupied");

            weapon w = weapon;
            w.coord.SetParent(null);
            weapon = null;
            return w;
        }
    }

    [inked]
    public class inv0 : inventory
    {
        [link]
        warrior warrior;

        public weapon_place[] sword_place { private set; get; }
        public weapon_place get_free_sword_place ()
        {
            for (int i = 0; i < sword_place.Length; i++)
            {
                if (!sword_place[i].occupied)
                    return sword_place[i];
            }
            return null;
        }

        public weapon_place[] bow_place { private set; get; }
        public weapon_place get_free_bow_place ()
        {
            for (int i = 0; i < bow_place.Length; i++)
            {
                if (!bow_place[i].occupied)
                    return bow_place[i];
            }
            return null;
        }

        public class ink : ink <inv0>
        {
            public ink (Transform[] swordplaces, Transform[] bowplaces)
            {
                o.sword_place = new weapon_place[swordplaces.Length];
                for (int i = 0; i < swordplaces.Length; i++)
                    o.sword_place[i] = new weapon_place(swordplaces[i]);

                o.bow_place = new weapon_place[bowplaces.Length];
                for (int i = 0; i < bowplaces.Length; i++)
                    o.bow_place[i] = new weapon_place(bowplaces[i]);
            }
        }

        public override void register_weapon (weapon weapon)
        {
            if (!free_place_for_exists (weapon))
                return;

            weapon.aquire ( warrior );
            get_free_place_for(weapon).put(weapon);
        }

        public bool free_place_for_exists (weapon weapon)
        {
            if (weapon is sword)
                for (int i = 0; i < sword_place.Length; i++)
                {
                    if (!sword_place[i].occupied)
                        return true;
                }

            if (weapon is bow)
                for (int i = 0; i < bow_place.Length; i++)
                {
                    if (!bow_place[i].occupied)
                        return true;
                }
            return false;
        }

        public weapon_place get_free_place_for (weapon weapon)
        {
            if (weapon is sword)
                return get_free_sword_place();
            else if (weapon is bow)
                return get_free_bow_place();
            return null;
        }
    }
}