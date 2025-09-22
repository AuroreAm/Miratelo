using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class mecha_buster : moon
    {
        public class ink : ink <mecha_buster>
        {
            public ink ( Transform orig, Transform end ) 
            {
                o.orig = orig;
                o.end = end;
            }
        }

        public static readonly term mecha_plasma = new term ( "arrow" );
        public const float plasma_speed = 50;
        public const float plasma_energy = .3f;

        Transform orig, end;
        public float roty => vecteur.rot_direction_y (orig.position, end.position);
        public Vector3 position => orig.position;
        
        float charge;
        float charge_speed = 1f;
        public bool charged => charge >= 1;
        public bool can_shot => charge > 0;

        public class aim : act
        {
            public override priority priority => priority.sub;

            // target aim rotation
            float roty;
            float speed => 720 * Time.deltaTime;

            [link]
            mecha_buster buster;
            [link]
            skin skin;

            protected override void _start()
            {
                skin.hold(new skin.animation(animation.begin_aim, this) { layer = skin.upper });
                at(skin.roty);
            }

            public void at (float y)
            {
                roty = y;
            }

            public void charge_buster ()
            {
                if ( on )
                buster.charge += buster.charge_speed * Time.deltaTime;
            }

            public void shot ()
            {
                if (!on) return;

                if ( buster.charge <= 0 ) return;

                buster.charge -= plasma_energy;

                arrow.fire ( mecha_plasma, buster.position, Quaternion.Euler ( new Vector3 (0, roty, 0) ), plasma_speed );
            }

            protected override void _stop()
            {
                skin.stop (skin.upper);
            }
        }
    }
}