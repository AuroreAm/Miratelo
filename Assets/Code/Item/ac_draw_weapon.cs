using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// draw a weapon in ms.r_arm
    /// </summary>
    [Unique]
    public class ac_draw_weapon : motor
    {
        public override int Priority => Pri.SubAction;

        [Depend]
        m_cortex mc;

        [Depend]
        m_sword_user msu;
        [Depend]
        m_bow_user mbu;

        [Depend]
        m_equip me;

        [Depend]
        m_skin ms;
        WeaponPlace from;
        SuperKey DrawAnimation;

        protected override void BeginStep()
        {
            if (me.weaponUser != null)
            Debug.LogError("the character have already equiped a weapon");
            if ( from == null )
            throw new InvalidOperationException ("No place to take weapon, must set the place before doing this action");
            ms.PlayState ( ms.r_arm, DrawAnimation, 0.1f, null, null, done );
        }

        public void SetPlaceToDrawFrom ( WeaponPlace Place )
        {
            from = Place;
            DrawAnimation = Place.Get().DefaultDrawAnimation;
        }

        protected override void Stop()
        {
            var mwu = GetCorrespondingWeaponUser ( from.Get().Type );
            mwu.SetWeaponBase ( from.Free() );
            me.SetWeaponUser ( mwu );

            mc.cortex.TriggerThinking ();

            from = null;
        }

        protected override void Abort()
        {
            from = null;
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