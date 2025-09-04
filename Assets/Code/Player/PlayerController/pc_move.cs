using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [ verse ("player controller") ]
    public class set_as_camera_subject : act
    {
        protected override void awaken()
        {
            s_camera.o.TpsACharacter ( sky.get <d_dimension_meta> () );
            sleep ();
        }
    }

    [ verse ("player controller") ]
    public class pc_move : act, ILucid
    {
        [harmony]
        ac_ground_complex groundMove;
        [harmony]
        kinesis motor;

        float speed = 7;

        protected override void alive()
        {
            if (motor.state == null)
                motor.perform ( groundMove, this );

            Vector3 InputAxis = Player.MoveAxis3;
            float runFactor = Player.Dash.Active ? WalkFactor.sprint : ( InputAxis.magnitude > 0.7f ? WalkFactor.run : WalkFactor.walk );
            InputAxis.Normalize ();

            InputAxis = Vecteur.LDir ( s_camera.o.td.RotY, InputAxis ) * speed;

            if (motor.state == groundMove)
            groundMove.Walk (InputAxis, runFactor );

            else if (motor.state is ac_fall fall)
            fall.AirMove (InputAxis );

            else if (motor.state is ac_jump jump)
            jump.AirMove (InputAxis );
        }

        public void inhalt(motor m) {}
    }
}