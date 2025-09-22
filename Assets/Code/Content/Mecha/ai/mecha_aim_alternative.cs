using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;
/*
namespace Triheroes.Code.Mecha
{
    [path("mecha ai")]
    public class mecha_aim_alternative : action, acting
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
        aim aim;

        [link]
        mecha_buster buster;
        
        [link]
        warrior warrior;
        dimension target => ( warrior.target ).system.get <dimension> ();
        
        float target_roty => forward? A: B;
        float roty;
        bool forward;

        protected override void _start()
        {
            roty = buster.roty;
            forward = true;
        }

        protected override void _step()
        {
            if (!aim.on)
                motor.start_act ( aim, this );

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

        protected override void _stop()
        {
            if (aim.on)
            motor.stop_act (this);
        }

        public void _act_end(act a, bool replaced)
        {}
    }
}
*/