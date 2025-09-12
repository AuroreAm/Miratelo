using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    [path("mecha ai")]
    public class mecha_aim_target_angle : action
    {
        [export]
        public float A = -45;
        [export]
        public float B = 45;
        [export]
        public float speed = 360;

        
        [link]
        motor motor;

        [link]
        move move;

        [link]
        aim aim;

        [link]
        mecha_buster buster;
        
        [link]
        warrior warrior;
        dimension target => ( warrior.target ).system.get <dimension> ();
        
        float target_roty => forward? A: B;
        float roty;
        bool forward;

        protected override void _step()
        {
            if (motor.act == null)
            motor.start_act ( move );

            if (!aim.on)
                motor.start_act2nd ( aim );

            if ( !warrior.target)
            {
                stop ();
                return;
            }

            var target_rot = vecteur.rot_direction_y ( buster.position, target.position ) + target_roty;

            roty = Mathf.MoveTowardsAngle ( roty,  target_rot, speed * Time.deltaTime );

            if ( Mathf.DeltaAngle ( roty, target_rot ) == 0 )
            forward = !forward;

            aim.at ( roty );
        }
    }

    [path ("mecha ai")]
    public class if_player_approach_rappidly : action
    {
        [export]
        public term term;

        [link]
        warrior warrior;
        [link]
        dimension dimension;
        [link]
        move_point point;
        dimension target => warrior.target.system.get < dimension > ();

        float target_distance;

        protected override void _start ()
        {
            if (!warrior.target) return;

            target_distance = Vector3.Distance ( dimension.position, target.position );
        } 

        protected override void _step ()
        {
            if (!warrior.target) return;

            if ( Vector3.Distance ( dimension.position, target.position ) < target_distance - point.lastdir.magnitude * 2 )
                {
                    task_sequence.substitute ( term );
                }

            target_distance = Vector3.Distance ( dimension.position, target.position );
        }
    }
}
