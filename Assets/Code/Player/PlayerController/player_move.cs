using Lyra;
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
        actor_speed actor_speed;

        protected override void _start() {
            actor_speed.speed = 5.5f;
        }

        protected override void _step()
        {
            if (motor.act == null)
                motor.start ( move );

            Vector3 input = player.move;
            float runFactor = player.dash ? walk_factor.sprint : ( input.magnitude > 0.7f ? walk_factor.run : walk_factor.walk );

            input.Normalize ();
            input = vecteur.ldir ( tps.main_roty, input ) * actor_speed.speed;

            if (motor.act == move)
            move.walk (input, runFactor );

            else if (motor.act is fall fall)
            fall.move (input );

            else if (motor.act is jump jump)
            jump.move (input );
        }
    }
}