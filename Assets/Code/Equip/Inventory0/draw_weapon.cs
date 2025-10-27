using System;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code.Inv0Act
{
    [need_ready]
    public class draw_weapon : act
    {
        public override priority priority => priority.sub;

        [link]
        sword_user sword_user;
        [link]
        bow_user bow_user;
        [link]
        equip equip;
        [link]
        skin skin;

        weapon_place from;
        term draw_animation;

        public bool prepared => from != null;

        protected override void _start()
        {
            if (equip.weapon_user != null)
            Debug.LogError("the character have already equiped a weapon");
            if ( from == null )
            throw new InvalidOperationException ("No place to take weapon, must set the place before doing this action");

            skin.animation play = new skin.animation ( draw_animation, this )
            {
                layer = skin.r_arm,
                ev0 = stop
            };
            skin.play ( play );
        }

        public draw_weapon _ ( weapon_place _place )
        {
            if (on) return this;

            from = _place;
            draw_animation =  get_corresponding_animation ( _place.get() );

            ready_for_tick ();

            return this;
        }

        protected override void _stop ()
        {
            var mwu = get_corresponding_weapon_user ( from.get() );
            mwu._ ( from.free() );
            equip.link_weapon_user ( mwu );

            from = null;
        }

        protected override void _abort()
        {
            from = null;
        }

        static term get_corresponding_animation ( weapon weapon )
        {
            if ( weapon is sword )
            return anim.take_sword;
            if ( weapon is bow )
            return anim.take_bow;

            return new term ();
        }

        public weapon_user get_corresponding_weapon_user ( weapon weapon )
        {
            if ( weapon is sword )
            return sword_user;
            if ( weapon is bow )
            return bow_user;

            return null;
        }
    }
}
