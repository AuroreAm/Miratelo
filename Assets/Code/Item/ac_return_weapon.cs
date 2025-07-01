using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// return a weapon in ms.r_arm
    /// </summary>
    [Unique]
    public class ac_return_weapon : action
    {
        [Depend]
        m_cortex mc;

        [Depend]
        m_skin ms;
        [Depend]
        m_equip me;
        
        WeaponPlace to;

        SuperKey ReturnAnimation;

        protected override void BeginStep()
        {
            if (me.weaponUser == null)
            Debug.LogError("the character have no weapon to return");
            ms.PlayState ( ms.r_arm, ReturnAnimation, 0.1f, null, null, done );
        }

        public void SetPlaceToReturn ( WeaponPlace toPlace )
        {
            to = toPlace;
            ReturnAnimation = me.weaponUser.WeaponBase.DefaultReturnAnimation;
        }

        override protected void Stop()
        {
            Weapon w = me.weaponUser.WeaponBase;
            me.weaponUser.Free (me);
            me.weaponUser = null;
            mc.cortex.TriggerThinking ();

            to.Put(w);
            to = null;
        }

        override protected void Abort()
        {
            to = null;
        }

        void done ()
        {
            AppendStop ();
        }
    }
}