using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Unique]
    [NodeDescription("draw a weapon in ms.r_arm")]
    [Category("actor")]
    public class ac_draw_weapon : action
    {
        [Depend]
        m_sword_user msu;
        [Depend]
        m_bow_user mbu;

        [Depend]
        m_equip me;


        [Depend]
        m_skin ms;
        Weapon weapon;
        SuperKey DrawAnimation;

        protected override void BeginStep()
        {
            ms.PlayState ( ms.r_arm, DrawAnimation, 0.1f, null, null, done );
        }

        public void Set ( Weapon weapon )
        {
            this.weapon = weapon;
            DrawAnimation = weapon.DefaultDrawAnimation;
        }

        protected override void Stop()
        {
            me.weaponUser = GetCorrespondingWeaponUser ( weapon.Type );
            me.weaponUser.SetWeaponBase ( weapon );
            me.weaponUser.Aquire (me);

            weapon = null;
        }

        protected override void Abort()
        {
            weapon = null;
        }

        void done ()
        {
            AppendStop ();
        }

        m_weapon_user GetCorrespondingWeaponUser ( WeaponType Wt )
        {
            switch (Wt)
            {
                case WeaponType.Sword: return msu;
                case WeaponType.Bow: return mbu;
            }
            return null;
        }
    }
}