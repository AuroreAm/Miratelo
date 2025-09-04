using System;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_return_weapon : motor
    {
        public override int priority => Rank.SubAction;

        [harmony]
        s_skin skin;
        [harmony]
        s_equip equip;
        
        WeaponPlace to;

        term ReturnAnimation;

        public bool prepared => to != null;

        protected override void awaken()
        {
            if (equip.WeaponUser == null)
            Debug.LogError("the character have no weapon to return");
            if (to == null)
            throw new InvalidOperationException ("No place to return, must set the place before doing this action");

            
            SkinAnimation play = new SkinAnimation ( ReturnAnimation, this )
            {
                LayerIndex = skin.r_arm,
                Ev0 = sleep
            };

            skin.PlayState ( play );
        }

        public void SetPlaceToReturn ( WeaponPlace toPlace )
        {
            if (on) return;

            to = toPlace;
            ReturnAnimation = GetCorrespondingDefaultReturnAnimation ( equip.WeaponUser.WeaponBase );
        }

        
        static readonly term return_sword = new term ( "return_sword" );
        static readonly term return_bow = new term ( "return_bow" );
        static term GetCorrespondingDefaultReturnAnimation ( d_weapon_standard weapon )
        {
            if ( weapon is d_sword )
            return return_sword;
            if ( weapon is d_bow )
            return return_bow;

            return new term ();
        }

        override protected void asleep ()
        {
            d_weapon_standard w = equip.WeaponUser.WeaponBase;
            equip.RemoveWeaponUser ();

            to.Put(w);
            to = null;
        }

        override protected void afaint ()
        {
            to = null;
        }
    }
}