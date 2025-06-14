using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// Switch to the corresponding weapon user branch: mbu - msu
    /// </summary>
    public class t_mwu : action
    {
        [Depend]
        m_equip me;

        protected override bool Step()
        {
            if (me.weaponUser is m_sword_user)
            {
                selector.CurrentSelector.SwitchTo ( StateKey2.msu );
                return false;
            }
            
            if (me.weaponUser is m_bow_user)
            {
                selector.CurrentSelector.SwitchTo ( StateKey2.mbu );
                return false;
            }
            return false;
        }
    }

    /// <summary>
    /// Switch to default branch if no weapon user
    /// </summary>
    public class t_zero : action
    {
        
        [Depend]
        m_equip me;

        protected override bool Step()
        {
            if (me.weaponUser == null)
            selector.CurrentSelector.FallBack ();

            return false;
        }
    }

}