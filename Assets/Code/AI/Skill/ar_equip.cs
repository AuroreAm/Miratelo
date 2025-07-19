using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ar_equip_equip_from_inv_0 : reflexion, IMotorHandler
    {
        [Depend]
        t_return_from_se_to_inv_0 trfsti0;
        [Depend]
        t_draw_from_inv_0 tdfi0;

        [Depend]
        s_equip se;
        [Depend]
        s_inv_0 si;

        [Depend]
        ac_draw_weapon adw;

        [Depend]
        ac_return_weapon arw;

        [Depend]
        s_motor sm;

        public void OnMotorEnd(motor m)
        {
            if (m == arw && trfsti0.on)
            trfsti0.Finish ();

            if (m == adw && tdfi0.on)
            tdfi0.Finish ();
        }

        protected override void Step()
        {
            if ( trfsti0.on && !arw.on)
            {
                arw.SetPlaceToReturn (  si.GetFreePlaceFor( se.weaponUser.WeaponBase ) );
                sm.SetSecondState ( arw, this );
            }

            if ( tdfi0.on && !adw.on)
            {
                WeaponPlace wp = null;

                switch (tdfi0.WeaponType)
                {
                    case WeaponType.Sword: wp = si.SwordPlaces [tdfi0.index]; break;
                    case WeaponType.Bow: wp = si.BowPlaces [tdfi0.index]; break;
                }

                if (wp != null)
                {
                    adw.SetPlaceToDrawFrom(wp);
                    sm.SetSecondState ( adw, this );
                }
            }
        }
    }
}
