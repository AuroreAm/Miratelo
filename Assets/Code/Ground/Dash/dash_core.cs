using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;
using UnityEngine.UIElements;

namespace Triheroes.Code
{
    public class dash_core : moon {
        [link]
        stand stand;

        [link]
        axeal a;

        term [] animations;
        AnimationCurve curve;
        public float lenght = 5;
        public float fade = .05f;
        public float duration = .5f;

        public static Vector3 dir_of(direction direction) => (direction == direction.forward) ? Vector3.forward : (direction == direction.back) ? Vector3.back : (direction == direction.right) ? Vector3.right : Vector3.left;

        public void set_animations ( term forward = default, term right = default, term left = default, term back = default ) {
            animations = new term [4];
            animations [0] = forward;
            animations [1] = right;
            animations [2] = left;
            animations [3] = back;
        }

        public void set_curve ( term name ) {
            curve = res.curves.q ( name );
        }

        public dash_core ( term curve ) {
            set_curve (curve);
        }

        direction direction;

        public void prepare (direction _direction) {
            direction = _direction;
        }


        public term animation_of ( direction direction = direction.forward ) {
            return animations [ (int) direction];
        }

        
        public void start ( star user, bool hold_animation = false, bool use_stand = true, Action end = null ) {
            a.set_force ( force ( dir_of (direction) ) );
            if ( !hold_animation )
                skin_play ( user, end );
            else
                skin_hold ( user );
            if ( use_stand ) stand.use (user);
        }

        [link]
        skin skin;

        void skin_play ( star player, Action end = null ) {
            skin.play ( new skin.animation ( animation_of (direction), player )
                {
                    fade = fade,
                    end = end
                }
             );
        }

        void skin_hold ( star player ) {
            skin.play ( new skin.animation ( animation_of (direction), player ) {
                    fade = fade,
                });
        }

        public void skin_stand_update () {
            a.deviate_main_force ( vecteur.ldir(skin.roty, dir_of (direction)) );
            stand.rotate_skin ();
        }

        public force_curve_data force ( Vector3 dir ) {
            var f = new force_curve_data ( duration, curve, 1 );
            f.dir = dir * lenght;
            return f;
        }

        public void stop () {
            a.stop_main_force ();
        }

        public bool done => a.main_force.done;
    }
    
    public enum direction { forward = 0, right = 1, left = 2, back = 3 }
}
