using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // To do swap weapon by UI
    /// <summary>
    /// transition to: draw
    /// </summary>
    [Unique]
    public class t_draw : action
    {
        
        [Depend]
        m_equip me;
        [Depend]
        m_inv_0 mi;
        ac_draw_weapon draw => me.character.GetUnique <ac_draw_weapon> ();

        protected override bool Step()
        {
            if (Player.Action2.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && me.weaponUser == null)
                {
                    draw.SetPlaceToDrawFrom ( mi.SwordPlaces[freeSword] );
                    selector.CurrentSelector.SwitchTo (StateKey2.draw);
                }
            }
            return false;
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

    [Unique]
    public class t_swap_sword : action
    {
        [Depend]
        m_equip me;
        [Depend]
        m_inv_0 mi;

        public static readonly SuperKey sword_swap = new SuperKey("sword_swap");
        ac_draw_weapon draw => me.character.GetUnique <ac_draw_weapon> ();
        ac_return_weapon return_ => me.character.GetUnique <ac_return_weapon> ();

        override protected bool Step()
        {
            if (Player.HatDown.OnActive)
            {
                var freeSword = GetUsableSword();
                if (freeSword != -1 && me.weaponUser != null)
                {
                    return_.SetPlaceToReturn ( mi.GetFreePlaceFor( me.weaponUser.WeaponBase ) );
                    draw.SetPlaceToDrawFrom ( mi.SwordPlaces[freeSword] );
                    selector.CurrentSelector.SwitchTo (sword_swap);
                }
            }
            return false;
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

    /// <summary>
    /// transition to: return"
    /// </summary>
    [Unique]
    public class t_return : action
    {
        [Depend]
        m_equip me;
        
        [Depend]
        m_inv_0 mi;

        ac_return_weapon return_ => me.character.GetUnique <ac_return_weapon> ();

        protected override bool Step()
        {
            if (Player.Action1.OnActive && me.weaponUser != null)
            {
            return_.SetPlaceToReturn ( mi.GetFreePlaceFor( me.weaponUser.WeaponBase ) );
            selector.CurrentSelector.SwitchTo (StateKey2.return_);
            }
            return false;
        }
    }
}