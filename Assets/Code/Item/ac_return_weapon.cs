using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Unique]
    [NodeDescription("return a weapon in ms.r_arm")]
    [Category("actor")]
    public class ac_return_weapon : action
    {
        [Depend]
        m_skin ms;
        [Depend]
        m_equip me;
        
        Weapon weapon;

        SuperKey ReturnAnimation;

        protected override void BeginStep()
        {
            ms.PlayState ( ms.r_arm, ReturnAnimation, 0.1f, null, null, done );
        }

        public void Set ( Weapon weapon )
        {
            this.weapon = weapon;
            ReturnAnimation = weapon.DefaultReturnAnimation;
        }

        override protected void Stop()
        {
            me.weaponUser.Free (me);
            me.weaponUser = null;
            me.ReAttachWeapon (weapon);
            weapon = null;
        }

        override protected void Abort()
        {
            weapon = null;
        }

        void done ()
        {
            AppendStop ();
        }
    }
}