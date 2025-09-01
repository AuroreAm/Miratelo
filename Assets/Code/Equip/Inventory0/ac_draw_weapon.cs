using System;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// draw a weapon in ms.r_arm
    /// </summary>
    public class ac_draw_weapon : motor
    {
        public override int Priority => Pri.SubAction;

        [Depend]
        s_sword_user ssu;
        [Depend]
        s_bow_user sbu;

        [Depend]
        s_equip se;

        [Depend]
        s_skin ss;
        WeaponPlace from;
        term DrawAnimation;

        public bool prepared => from != null;

        protected override void Start()
        {
            if (se.weaponUser != null)
            Debug.LogError("the character have already equiped a weapon");
            if ( from == null )
            throw new InvalidOperationException ("No place to take weapon, must set the place before doing this action");
            ss.PlayState ( ss.r_arm, DrawAnimation, 0.1f, null, null, done );
        }

        public void SetPlaceToDrawFrom ( WeaponPlace Place )
        {
            if (on) return;

            from = Place;
            DrawAnimation = Place.Get().DefaultDrawAnimation;
        }

        protected override void Stop()
        {
            var mwu = GetCorrespondingWeaponUser ( from.Get().Type );
            mwu.SetWeaponBase ( from.Free() );
            se.SetWeaponUser ( mwu );

            from = null;
        }

        protected override void Abort()
        {
            from = null;
        }

        void done ()
        {
            if (on)
            SelfStop ();
        }

        s_weapon_user GetCorrespondingWeaponUser ( WeaponType Wt )
        {
            switch (Wt)
            {
                case WeaponType.Sword: return ssu;
                case WeaponType.Bow: return sbu;
            }
            return null;
        }
    }
}