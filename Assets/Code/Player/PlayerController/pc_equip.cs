using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [Path ("player controller")]
    public class pc_equip : action, IMotorHandler
    {
        [Link]
        s_motor motor;

        [Link]
        s_equip equip;
        [Link]
        d_inv_0 inventory;

        [Link]
        ac_draw_weapon drawWeapon;

        [Link]
        ac_return_weapon returnWeapon;

        public void OnMotorEnd(motor m)
        {
            if (m == returnWeapon && drawWeapon.prepared)
                motor.SetSecondState(drawWeapon, this);
        }

        protected override void OnStep()
        {
            if (Player.Action2.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && equip.WeaponUser == null)
                {
                    drawWeapon.SetPlaceToDrawFrom(inventory.SwordPlaces[freeSword]);
                    motor.SetSecondState(drawWeapon, this);
                }
            }

            if (Player.HatDown.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && equip.WeaponUser != null)
                {
                    returnWeapon.SetPlaceToReturn(inventory.GetFreePlaceFor(equip.WeaponUser.WeaponBase));
                    drawWeapon.SetPlaceToDrawFrom(inventory.SwordPlaces[freeSword]);

                    motor.SetSecondState(returnWeapon, this);
                }
            }

            if (Player.Aim.OnActive && inventory.BowPlaces[0].Occupied && equip.WeaponUser == null)
            {
                drawWeapon.SetPlaceToDrawFrom(inventory.BowPlaces[0]);
                motor.SetSecondState(drawWeapon, this);
            }

            if (Player.Action1.OnActive && equip.WeaponUser != null)
            {
                returnWeapon.SetPlaceToReturn(inventory.GetFreePlaceFor(equip.WeaponUser.WeaponBase));
                motor.SetSecondState(returnWeapon, this);
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