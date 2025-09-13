using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("ai")]
    public class look_target : action
    {
        [link]
        dimension dimension;
        [link]
        warrior warrior;
        [link]
        skin skin;
        [link]
        stand stand;
        
        [export]
        public float _angular_speed = 160;
        
        dimension target => warrior.target.get_dimension ();

        protected override void _start()
        {
            stand.use ( this );
        }

        protected override void _step ()
        {
            var rot = vecteur.rot_direction_y ( dimension.position,target.position );
            stand.roty = Mathf.MoveTowardsAngle(skin.roty, rot, Time.deltaTime * _angular_speed);

            stand.rotate_skin ();
            
            if ( rot == skin.roty || !stand.active )
            stop ();
        }
    }

    [path ("ai")]
    public class look_target_normal : look_target
    {
        [link]
        move move;

        [link]
        motor motor;

        protected override void _start()
        {
            base._start();
            motor.start_act (move);
        }
    }
}
