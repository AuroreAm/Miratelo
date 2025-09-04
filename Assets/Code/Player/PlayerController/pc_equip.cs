using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [verse ("player controller")]
    public class pc_equip : act, ILucid
    {
        [harmony]
        kinesis motor;

        [harmony]
        s_equip equip;
        [harmony]
        d_inv_0 inventory;

        [harmony]
        ac_draw_weapon drawWeapon;

        [harmony]
        ac_return_weapon returnWeapon;

        public void inhalt(motor m)
        {
            if (m == returnWeapon && drawWeapon.prepared)
                motor.perform2nd(drawWeapon, this);
        }

        protected override void alive()
        {
            if (Player.Action2.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && equip.WeaponUser == null)
                {
                    drawWeapon.SetPlaceToDrawFrom(inventory.SwordPlaces[freeSword]);
                    motor.perform2nd(drawWeapon, this);
                }
            }

            if (Player.HatDown.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && equip.WeaponUser != null)
                {
                    returnWeapon.SetPlaceToReturn(inventory.GetFreePlaceFor(equip.WeaponUser.WeaponBase));
                    drawWeapon.SetPlaceToDrawFrom(inventory.SwordPlaces[freeSword]);

                    motor.perform2nd(returnWeapon, this);
                }
            }

            if (Player.Aim.OnActive && inventory.BowPlaces[0].Occupied && equip.WeaponUser == null)
            {
                drawWeapon.SetPlaceToDrawFrom(inventory.BowPlaces[0]);
                motor.perform2nd(drawWeapon, this);
            }

            if (Player.Action1.OnActive && equip.WeaponUser != null)
            {
                returnWeapon.SetPlaceToReturn(inventory.GetFreePlaceFor(equip.WeaponUser.WeaponBase));
                motor.perform2nd(returnWeapon, this);
            }
        }

        int GetUsableSword()
        {
            for (int i = 0; i < inventory.SwordPlaces.Length; i++)
            {
                if (inventory.SwordPlaces[i].Occupied)
                    return i;
            }
            return -1;
        }
    }
}