using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class pr_equip : reflexion, IMotorHandler
    {
        [Depend]
        s_mind sm;
        
        [Depend]
        s_motor m;

        [Depend]
        s_equip se;
        [Depend]
        s_inv_0 si;
        
        [Depend]
        ac_draw_weapon adw;

        [Depend]
        ac_return_weapon arw;

        public void OnMotorEnd(motor m)
        {
            if ( m == arw && adw.prepared )
            this.m.SetSecondState ( adw, this );
        }

        protected override void Step()
        {
            if (Player.Action2.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && se.weaponUser == null)
                {
                    adw.SetPlaceToDrawFrom ( si.SwordPlaces[freeSword] );
                    m.SetSecondState ( adw, this );
                }
            }

            if (Player.HatDown.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && se.weaponUser != null)
                {
                    arw.SetPlaceToReturn(si.GetFreePlaceFor(se.weaponUser.WeaponBase));
                    adw.SetPlaceToDrawFrom(si.SwordPlaces[freeSword]);

                    m.SetSecondState ( arw, this );
                }
            }

            if (Player.Aim.OnActive && si.BowPlaces [0].Occupied && se.weaponUser == null)
            {
                adw.SetPlaceToDrawFrom ( si.BowPlaces [0] );
                m.SetSecondState ( adw, this );
            }

            if (Player.Action1.OnActive && se.weaponUser != null)
            {
                arw.SetPlaceToReturn ( si.GetFreePlaceFor( se.weaponUser.WeaponBase ) );
                m.SetSecondState ( arw, this );
            }
        }

        int GetUsableSword ()
        {
            for (int i = 0; i < si.SwordPlaces.Length; i++)
            {
                if (si.SwordPlaces[i].Occupied)
                    return i;
            }
            return -1;
        }
    }

    public class pr_interact_near_weapon : reflexion
    {
        [Depend]
        s_inv_0 si;

        protected override void Step()
        {
            if (play.o.currentInteractable is pi_weapon pw)
            {
                if (si.FreePlaceExistFor(pw.weapon))
                {
                    gf_interact.ShowInteractText( string.Concat (" take ", pw.weapon.Name) );
                    if (Player.Action1.OnActive)
                    si.RegisterWeapon (pw.weapon);
                }
            }
        }
    }
}