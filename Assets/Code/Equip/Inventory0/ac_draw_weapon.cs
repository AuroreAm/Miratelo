using System;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public class ac_draw_weapon : motor
    {
        public override int Priority => Rank.SubAction;

        [Link]
        s_sword_user swordUser;
        [Link]
        s_bow_user bowUser;

        [Link]
        s_equip equip;

        [Link]
        s_skin skin;

        WeaponPlace from;
        term DrawAnimation;

        public bool prepared => from != null;

        protected override void OnStart()
        {
            if (equip.WeaponUser != null)
            Debug.LogError("the character have already equiped a weapon");
            if ( from == null )
            throw new InvalidOperationException ("No place to take weapon, must set the place before doing this action");

            SkinAnimation play = new SkinAnimation ( DrawAnimation, this )
            {
                LayerIndex = skin.r_arm,
                Ev0 = Stop
            };
            skin.PlayState ( play );
        }

        public void SetPlaceToDrawFrom ( WeaponPlace Place )
        {
            if (on) return;

            from = Place;
            DrawAnimation =  GetCorrespondingDefaultDrawAnimation ( Place.Get() );
        }

        protected override void OnStop ()
        {
            var mwu = GetCorrespondingWeaponUser ( from.Get() );
            mwu.SetWeaponBase ( from.Free() );
            equip.SetWeaponUser ( mwu );

            from = null;
        }

        protected override void OnAbort()
        {
            from = null;
        }

        static readonly term take_sword = new term ( "take_sword" );
        static readonly term take_bow = new term ( "take_bow" );
        static term GetCorrespondingDefaultDrawAnimation ( d_weapon_standard weapon )
        {
            if ( weapon is d_sword )
            return take_sword;
            if ( weapon is d_bow )
            return take_bow;

            return new term ();
        }

        s_weapon_user GetCorrespondingWeaponUser ( d_weapon_standard weapon )
        {
            if ( weapon is d_sword )
            return swordUser;
            if ( weapon is d_bow )
            return bowUser;

            return null;
        }
    }
}
