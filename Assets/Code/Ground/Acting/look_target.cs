using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("move")]
    public class look_target : action
    {
        [link]
        character c;
        [link]
        warrior warrior;
        [link]
        skin skin;
        [link]
        stand stand;
        
        [export]
        public float _angular_speed = 160;
        
        character target => warrior.target.c;

        protected override void _start()
        {
            stand.use ( this );
        }

        protected override void _step ()
        {
            var rot = vecteur.rot_direction_y ( c.position,target.position );
            stand.roty = Mathf.MoveTowardsAngle(skin.roty, rot, Time.deltaTime * _angular_speed);

            stand.rotate_skin ();
            
            if ( rot == skin.roty || !stand.active )
            stop ();
        }
    }
}
