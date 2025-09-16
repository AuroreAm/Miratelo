using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    [path("mecha ai")]
    public class ai_aim_target : action, acting
    {
        [link]
        motor motor;
        [link]
        aim aim;
        [link]
        warrior warrior;
        [link]
        mecha_buster buster;
        
        dimension target => warrior.target.get_dimension ();

        public void _act_end(act a, bool replaced)
        {}

        protected override void _step()
        {
            if (!aim.on)
                motor.start_act ( aim, this );

            if ( !warrior.target)
            {
                stop ();
                return;
            }

            aim.at ( vecteur.rot_direction_y ( buster.position, target.position ) );
        }

        protected override void _stop()
        {
            if (aim.on)
            motor.stop_act ( this );
        }
    }
}
