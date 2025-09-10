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

        Transform orig, end;
        public Vector3 rot => vecteur.rot_direction (orig.position, end.position);
        public float roty => vecteur.rot_direction_y (orig.position, end.position);
        public Vector3 position => orig.position;

        public class charger : controller
        {
            [link]
            mecha_buster buster;

            readonly term spectre = new term ( "sp_charge" );

            float charge;
            float charge_speed = .75f;
            public bool charged => charge >= 1;
            public bool can_shot => charge > 0;

            int illusion_id;

            protected override void _start()
            {
                charge = 0;
                illusion_id = illusion.fire ( spectre, buster.position );
            }

            protected override void _step ()
            {
                charge += Time.deltaTime * charge_speed;
            }

            protected override void _stop()
            {
                illusion.stop ( illusion_id );
            }

            public void shot ()
            {
                if ( charge <= 0 ) return;
                charge -= .025f;
                arrow.fire ( mecha_plasma, buster.position, Quaternion.Euler (buster.rot), 30 );
            }
        }
    }
}