using Lyra;
using System;
using UnityEngine;

namespace Triheroes.Code.Inv0Act
{
    public class return_weapon : act
    {
        public override priority priority => priority.sub;

        [link]
        skin skin;
        [link]
        equip equip;
        
        weapon_place to;

        term return_animation;

        public bool prepared => to != null;

        protected override void _start()
        {
            if (equip.weapon_user == null)
            Debug.LogError("the character have no weapon to return");
            if (to == null)
            throw new InvalidOperationException ("No place to return, must set the place before doing this action");

            
            skin.animation play = new skin.animation ( return_animation, this )
            {
                layer = skin.r_arm,
                ev0 = stop
            };

            skin.play ( play );
        }

        public void set_place ( weapon_place _place )
        {
            if (on) return;

            to = _place;
            return_animation = get_corresponding_animation ( equip.weapon_user.weapon_base );
        }

        static term get_corresponding_animation ( weapon weapon )
        {
            if ( weapon is sword )
            return animation.return_sword;
            if ( weapon is bow )
            return animation.return_bow;

            return new term ();
        }

        override protected void _stop ()
        {
            weapon w = equip.weapon_user.weapon_base;
            equip.remove_weapon_user ();

            to.put(w);
            to = null;
        }

        override protected void _abort ()
        {
            to = null;
        }
    }
}