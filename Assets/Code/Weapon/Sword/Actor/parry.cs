using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.Sword.Combat
{
    public class parry : act
    {
        public override priority priority => priority.action;

        [link]
        sword_user sword_user;
        [link]
        skin skin;

        term parry_animation = animation.SS8_0;

        public parry (term _parry_animation)
        {
            parry_animation = _parry_animation;
        }

        protected override void _start ()
        {
            skin.play ( new skin.animation ( parry_animation, this )
            {
                end = stop,
                ev0 = start_parry,
                ev1 = end_parry
            } );
        }

        void start_parry ()
        {
            sword_user.weapon.enable_parry ();
        }

        void end_parry ()
        {
            sword_user.weapon.disable_parry ();
        }

        protected override void _stop ()
        {
            sword_user.weapon.disable_parry ();
        }
    }

    public struct parried
    {}

    public class parry_arrow : act
    {
        public override priority priority => priority.action;

        [link]
        sword_user sword_user;
        [link]
        skin skin;

        [link]
        arrow_alert alert;

        bool can_parry;

        term anim;

        public parry_arrow ( term animation )
        {
            anim = animation;
        }

        protected override void _start ()
        {
            skin.play ( new skin.animation ( anim, this )
            {
                end = stop,
                ev0 = active_parry,
                fade = .05f
            } );
        }

        void active_parry ()
        {
            can_parry = true;
        }

        protected override void _step()
        {
            if ( !alert.alert || !can_parry ) return;

            if ( Vector3.Distance ( alert.position, sword_user.weapon.position ) < ( sword_user.weapon.length + ( alert.speed * Time.deltaTime ) ) && Mathf.Abs ( Mathf.DeltaAngle ( vecteur.rot_direction_y ( skin.position, alert.position ), skin.roty ) ) < 90 )
            {
                arrow.deflect ( alert.position, vecteur.ldir ( skin.roty, Vector3.forward ));
                can_parry = false;
            }
        }
    }
}