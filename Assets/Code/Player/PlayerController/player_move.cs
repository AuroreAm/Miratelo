using Lyra;
using Triheroes.Code.CapsuleAct;
using UnityEngine;

namespace Triheroes.Code
{
    [ path ("player controller") ]
    public class set_as_camera_subject : action
    {
        protected override void _start()
        {
            camera.o.start_player_camera ( system.get <dimension> () );
            stop ();
        }
    }

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

            Vector3 InputAxis = player.move;
            float runFactor = player.dash ? walk_factor.sprint : ( InputAxis.magnitude > 0.7f ? walk_factor.run : walk_factor.walk );
            InputAxis.Normalize ();

            InputAxis = vecteur.ldir ( camera.o.tps_roty, InputAxis ) * speed;

            if (motor.act == move)
            move.walk (InputAxis, runFactor );

            else if (motor.act is fall fall)
            fall.move (InputAxis );

            else if (motor.act is jump jump)
            jump.move (InputAxis );
        }
    }
}