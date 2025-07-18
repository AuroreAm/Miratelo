using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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


        public override void Create()
        {
            bool draw_condition ()
            {
                return se.weaponUser == null && adw.prepared;
            }

            bool return_condition ()
            {
                return se.weaponUser != null && arw.prepared;
            }

            var draw_notion = new plan_notion ( new Notion ( draw_condition, commands.return_weapon ) , new motor_second_task (adw) , commands.draw_weapon );
            
            var return_notion = new plan_notion ( new Notion ( return_condition, commands.draw_weapon ) , new  motor_second_task (arw) , commands.return_weapon );

            // NOTE: draw weapon doesn't provide solution for preparation, so the task will be failled infinitelly if the actions are not prepared, just putting this note here for an eventual case where some other module relly on these tasks

            sm.AddNotion ( draw_notion );
            sm.AddNotion ( return_notion );
        }

        public void OnMotorEnd(motor m)
        {
        }

        protected override void Step()
        {
            if (Player.Action2.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && se.weaponUser == null)
                {
                    adw.SetPlaceToDrawFrom ( si.SwordPlaces[freeSword] );
                    m.SetSecondState (adw, this);
                }
            }

            if (Player.HatDown.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && se.weaponUser != null)
                {
                    arw.SetPlaceToReturn(si.GetFreePlaceFor(se.weaponUser.WeaponBase));
                    adw.SetPlaceToDrawFrom(si.SwordPlaces[freeSword]);

                    sm.DoTask ( commands.draw_weapon );
                }
            }

            if (Player.Aim.OnActive && si.BowPlaces [0].Occupied && se.weaponUser == null)
            {
                adw.SetPlaceToDrawFrom ( si.BowPlaces [0] );
                m.SetSecondState (adw,this);
            }

            if (Player.Action1.OnActive && se.weaponUser != null)
            {
                arw.SetPlaceToReturn ( si.GetFreePlaceFor( se.weaponUser.WeaponBase ) );
                m.SetSecondState (arw, this);
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