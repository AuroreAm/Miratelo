using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [ Path ("player controller") ]
    public class set_as_camera_subject : action
    {
        protected override void OnStart()
        {
            s_camera.o.TpsACharacter ( Structure.Get <d_dimension_meta> () );
            Stop ();
        }
    }

    [ Path ("player controller") ]
    public class pc_move : action, IMotorHandler
    {
        [Link]
        ac_ground_complex groundMove;
        [Link]
        s_motor motor;

        float speed = 7;

        protected override void OnStep()
        {
            if (motor.State == null)
                motor.SetState ( groundMove, this );

            Vector3 InputAxis = Player.MoveAxis3;
            float runFactor = Player.Dash.Active ? WalkFactor.sprint : ( InputAxis.magnitude > 0.7f ? WalkFactor.run : WalkFactor.walk );
            InputAxis.Normalize ();

            InputAxis = Vecteur.LDir ( s_camera.o.td.RotY, InputAxis ) * speed;

            if (motor.State == groundMove)
            groundMove.Walk (InputAxis, runFactor );

            else if (motor.State is ac_fall fall)
            fall.AirMove (InputAxis );

            else if (motor.State is ac_jump jump)
            jump.AirMove (InputAxis );
        }

        public void OnMotorEnd(motor m) {}
    }
}