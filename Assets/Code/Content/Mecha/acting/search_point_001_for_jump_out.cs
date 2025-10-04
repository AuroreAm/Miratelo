using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // TODO documentation
    // if this doesnt find a point, this stop
    [path ("ai")]
    public class search_point_001_for_jump_out : action {
        [link]
        point_001 point;

        [link]
        character c;
        [link]
        warrior warrior;

        [export]
        public float target_distance = 4;

        [export]
        public float jump_out_distance = 15;

        protected override void _start() {
            
            float offset;
            offset = vecteur.rot_direction_y ( warrior.target.c.position, c.position );

            for (float angle = 0; angle < 90; angle += 10) {
                if ( raycast (angle + offset, out point.pos ) ) return;
                if ( raycast (-angle + offset, out point.pos ) ) return;
            }

            stop ();

        }

        bool raycast ( float angle, out Vector3 position ) {
            position = Vector3.zero;
            // check if target distance is a valid place to jump out
            if ( Physics.Raycast ( warrior.target.c.position, vecteur.ldir ( angle, Vector3.forward ), target_distance, vecteur.Solid ) )
            return false;

            // check if jump_out distance is a valid place to jump out
            if ( Physics.Raycast ( warrior.target.c.position, vecteur.ldir ( angle, Vector3.forward ), jump_out_distance + target_distance, vecteur.Solid ) )
            return false;

            position = warrior.target.c.position + vecteur.ldir ( angle, Vector3.forward * target_distance );
            return true;
        }
    }
}