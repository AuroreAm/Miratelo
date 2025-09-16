using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code
{
    [ path ("player controller") ]
    public class player_move : action
    {
        [link]
        move move;
        [link]
        motor motor;

        [link]
        react_knock react_knock;

        float speed = 7;
        protected override void _step()
        {
            if (motor.act == null)
                motor.start_act ( move );

            Vector3 input = player.move;
            float runFactor = player.dash ? walk_factor.sprint : ( input.magnitude > 0.7f ? walk_factor.run : walk_factor.walk );
            input.Normalize ();

            input = vecteur.ldir ( camera.o.tps_roty, input ) * speed;

            if (motor.act == move)
            move.walk (input, runFactor );

            else if (motor.act is fall fall)
            fall.move (input );

            else if (motor.act is jump jump)
            jump.move (input );
        }
    }
}