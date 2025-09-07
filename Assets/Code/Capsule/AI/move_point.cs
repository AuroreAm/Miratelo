using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.CapsuleAct
{
    public class move_point : controller, acting
    {
        [link]
        move move;
        [link]
        motor motor;
        [link]
        dimension dimension;

        List <Vector3> points = new List<Vector3> ();
        public float speed = 7;

        public int count => points.Count;
        public void set_point (int i, Vector3 point)
        {
            points [i] = point;
        }
        
        /// <summary> last direction the character moved</summary>
        Vector3 lastdir;

        protected override void _step()
        {

            if ( points.Count != 0 && !move.on )
                motor.start_act ( move, this );

            if ( points.Count == 0 || !move.on ) return;
            
            Vector3 direction = ( points [0].xz () - dimension.position.xz () ).normalized;
            direction = direction * speed;
            move.walk ( direction );
            lastdir = direction * Time.deltaTime;

            while ( Vector3.Distance ( dimension.position.xz (), points  [0].xz () ) < lastdir.magnitude + .5f )
            {
                points.RemoveAt (0);
                if (points.Count == 0)
                break;
            }
        }

        // TODO impossible path notice
        
        protected override void _stop()
        {
            lastdir = Vector3.zero;
        }

        public void set_way ( Vector3 [] points )
        {
            this.points.Clear ();
            this.points.AddRange ( points );
        }

        public void clear ()
        {
            points.Clear ();
        }

        public void _act_end(act m)
        {}
    }
}
