using Lyra;
using UnityEngine;
using Triheroes.Code.CapsuleAct;
using System.Collections;
using System.Collections.Generic;

namespace Triheroes.Code
{
    [path ("AI")]
    public class move_around_target : action
    {
        [export]
        public float _angle_amount;
        [export]
        public float _distance;

        
        [link]
        warrior warrior;
        [link]
        character c;
        [link]
        move_point point;

        int way_counts;
        float roty_start;

        character target => warrior.target.c;

        protected override void _start()
        {
            link (point);

            if ( !warrior.target ) return;

            roty_start = vecteur.rot_direction_y ( target.position, c.position );

            way_counts =  1 + (int) _angle_amount / 10;
            var points = new Vector3 [ way_counts ];
            point.set_way ( points );

            circle_way ();
        }

        protected override void _step()
        {
            if ( !warrior.target || point.count == 0 )
            {
                stop ();
                return;
            }

            circle_way (2); 
        }

        void circle_way ( int start = 0 )
        {
            for (int i = start; i < point.count - 1; i++)
            {
                point.set_point (i, target.position + vecteur.ldir ( new Vector3(0,roty_start + (i + way_counts - point.count ) * 10,0), Vector3.forward * _distance ) );
            }

            if ( point.count > 0 )
            point.set_point ( point.count - 1, target.position + vecteur.ldir ( new Vector3(0,roty_start + _angle_amount ,0), Vector3.forward * _distance ) );
        }
    }
}
