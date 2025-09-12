using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.CapsuleAct
{
    public class dash : act
    {
        public override int priority => level.action;

        static term dash_animation_of (direction direction) => (direction == direction.forward)? animation.dash_forward : (direction == direction.right)? animation.dash_right : (direction == direction.left)? animation.dash_left : animation.dash_back;
        public static Vector3 dir_of ( direction direction ) => (direction == direction.forward)? Vector3.forward : (direction == direction.back)? Vector3.back:(direction == direction.right)? Vector3.right:Vector3.left;

        
        [link]
        capsule capsule;
        [link]
        skin skin;
        [link]
        stand stand;

        public Vector3 dir;
        direction direction;
        delta_curve cu;
        term dash_animation;

        float fade;

        public void set ( direction direction )
        {
            this.direction = direction;
            dash_animation = dash_animation_of (this.direction);
            dir = dir_of (this.direction);
            fade = 0.05f;
        }

        public void override_animation ( term animation ) => dash_animation = animation;
        public void override_animation ( term animation, float transitionDuration )
        {
            dash_animation = animation;
            fade = transitionDuration;
        }

        protected override void _ready ()
        {
            cu = new delta_curve( triheroes_res.curve.q( new term("jump") ).curve );
        }

        protected override void _start ()
        {
            this.link (capsule);

            stand.use (this);
            skin.play ( new skin.animation ( dash_animation, this ) {fade = fade , end = stop } );
            cu.start ( 5, skin.duration ( dash_animation ) );
        }

        protected override void _step ()
        {
            capsule.dir += vecteur.ldir ( skin.roty,dir ) * cu.tick_delta ();
            stand.rotate_skin ();
        }
    }

    public enum direction { forward, right , left , back }
}