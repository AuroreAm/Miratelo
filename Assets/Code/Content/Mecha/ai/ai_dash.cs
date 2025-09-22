using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code
{
    
    [path("mecha ai")]
    public class ai_dash : action
    {
        [link]
        dash dash;

        [link]
        motor motor;

        [export]
        public direction direction;

        protected override void _start()
        {
            motor.start_act (dash._(direction));
        }

        protected override void _step()
        {
            if (!dash.on)
            stop ();
        }
    }

    
    [path("mecha ai")]
    public class ai_dash_steer_arround_target : action
    {

        [link]
        warrior warrior;
        [link]
        stand stand;

        [link]
        dimension dimension;
        float direction;

        dimension target => warrior.target.get_dimension();

        protected override void _start()
        {
            direction = stand.roty - vecteur.rot_direction_y ( dimension.position, target.position );
        }

        protected override void _step()
        {
            if (!warrior.target)
            {
                stop();
                return;
            }

            stand.roty = vecteur.rot_direction_y ( dimension.position, target.position ) + direction;
        }

    }
}
