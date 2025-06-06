using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Unique]
    [NodeDescription("transition to: draw")]
    [Category("player controller")]
    public class t_draw : action
    {
        [Depend]
        m_equip me;

        ac_draw_weapon draw => me.character.GetUnique <ac_draw_weapon> ();

        protected override bool Step()
        {
            if (Player.GetButtonDown(BoutonId.A) && me.weaponUser == null)
            {
                draw.Set ( me.weapons [0] );
                selector.CurrentSelector.SwitchTo (StateKey2.draw);
            }
            return false;
        }
    }

    [Unique]
    
    [NodeDescription("transition to: return")]
    [Category("player controller")]
    public class t_return : action
    {
        [Depend]
        m_equip me;

        ac_return_weapon return_ => me.character.GetUnique <ac_return_weapon> ();

        protected override bool Step()
        {
            if (Player.GetButtonDown(BoutonId.A) && me.weaponUser != null)
            {
            return_.Set ( me.weaponUser.WeaponBase );
            selector.CurrentSelector.SwitchTo (StateKey2.return_);
            }
            return false;
        }
    }
}