using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pixify;

namespace Triheroes.Code
{
    public class pr_equip : reflection
    {
        [Depend]
        m_equip me;
        [Depend]
        m_inv_0 mi;
        
        [Depend]
        ac_draw_weapon adw;

        [Depend]
        ac_return_weapon arw;

        [Depend]
        m_cortex mc;

        //INPROGRESS
        /*
        public override void Create()
        {
            TreeStart ( me.character );
            new sequence () {repeat = false};
                new ac_return_weapon ();
                new ac_draw_weapon ();
            end ();
            swap_sword = TreeFinalize ();
        }*/

        public override void Main()
        {
            if (Player.Action2.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && me.weaponUser == null)
                {
                    adw.SetPlaceToDrawFrom ( mi.SwordPlaces[freeSword] );
                    mm.SetSecondState (adw);
                }
            }

            //INPROGRESS
            /*
            if (Player.HatDown.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && me.weaponUser != null)
                {
                    arw.SetPlaceToReturn ( mi.GetFreePlaceFor( me.weaponUser.WeaponBase ) );
                    adw.SetPlaceToDrawFrom ( mi.SwordPlaces[freeSword] );
                    mm.SetSecondState (swap_sword,Pri.SubAction);
                }
            }*/

            if (Player.Aim.OnActive && mi.BowPlaces [0].Occupied && me.weaponUser == null)
            {
                adw.SetPlaceToDrawFrom ( mi.BowPlaces [0] );
                mm.SetSecondState (adw);
            }

            if (Player.Action1.OnActive && me.weaponUser != null)
            {
                arw.SetPlaceToReturn ( mi.GetFreePlaceFor( me.weaponUser.WeaponBase ) );
                mm.SetSecondState (arw);
            }
        }

        int GetUsableSword ()
        {
            for (int i = 0; i < mi.SwordPlaces.Length; i++)
            {
                if (mi.SwordPlaces[i].Occupied)
                    return i;
            }
            return -1;
        }
    }

    public class pr_interact_near_weapon : reflection
    {
        [Depend]
        m_inv_0 mi;

        public override void Main()
        {
            if (play.o.currentInteractable is pi_weapon pw)
            {
                if (mi.FreePlaceExistFor(pw.weapon))
                {
                    gf_interact.ShowInteractText( string.Concat (" take ", pw.weapon.Name) );
                    if (Player.Action1.OnActive)
                    mi.RegisterWeapon (pw.weapon);
                }
            }
        }
    }
}