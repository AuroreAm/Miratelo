using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public class player_lateral : controller , act_handler
    {
        [link]
        motor motor;

        [link]
        lateral lateral;

        public float speed = 6; 

        protected override void _step ()
        {
            if ( !lateral.on )
            motor.start_act ( lateral, this );

            Vector3 input;
            input = player.move;
            input = vecteur.ldir ( camera.o.tps_roty, input) * 6f;

            lateral.walk_lateral ( input );
        }

        protected override void _stop ()
        {
            if ( motor.act == lateral )
            motor.stop_act ( this );
        }

        public void _act_end(act a, act_status status) {
        }
    }
}
