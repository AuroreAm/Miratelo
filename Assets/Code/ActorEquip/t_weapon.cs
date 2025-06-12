using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("actor")]
    [NodeDescription("Switch to the corresponding weapon user branch: mbu - msu ")]
    public class t_mwu : action
    {
        [Depend]
        m_equip me;
        public static readonly SuperKey msu = new SuperKey ("msu");
        public static readonly SuperKey mbu = new SuperKey ("mbu");

        protected override bool Step()
        {
            if (me.weaponUser is m_sword_user)
            {
                selector.CurrentSelector.SwitchTo ( msu );
                return false;
            }
            
            if (me.weaponUser is m_bow_user)
            {
                selector.CurrentSelector.SwitchTo ( mbu );
                return false;
            }
            return false;
        }
    }

    [Category("actor")]
    [NodeDescription("Switch to default branch if no weapon user")]
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