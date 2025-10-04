using UnityEngine;
using Lyra;

// refer to the documentation for more information about indexed point
namespace Triheroes.Code {
    public class point_001 : moon {
        public Vector3 pos;
    }

    [path ("ai")]
    public class refresh_point_001 : action {
        [link]
        choose_point_001_left_right_of_target choose;

        protected override void _start() {
            choose.calculate_pos ();
            stop ();
        }
    }

    public class look_point_001 : look {

        [link]
        point_001 point;

        protected override float get_rot_y() {
            return vecteur.rot_direction_y ( c.position, point.pos );
        }
        
    }

    public class move_to_point_001 : move_to {
        [link]
        point_001 point;

        protected override Vector3 get_target() {
            return point.pos;
        }
    }

    [path ("ai")]
    public class if_distance_point_001_less_than : action {
        [link]
        character c;

        [link]
        point_001 point;

        [export]
        public float distance = 5;

        protected override void _step() {
            if ( Vector3.Distance ( c.position, point.pos ) < distance ) stop ();
        }
    }
}