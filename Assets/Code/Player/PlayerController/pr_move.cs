using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_move : reflexion, IMotorHandler
    {
        [Depend]
        ac_ground_complex agc;
        [Depend]
        s_motor sm;

        float speed = 7;

        public void OnMotorEnd(motor m)
        {}

        protected override void Reflex()
        {
            if (sm.state == null)
                sm.SetState (agc,this);

            Vector3 InputAxis = Player.MoveAxis3;
            float runFactor = Player.Dash.Active ? WalkFactor.sprint : ( InputAxis.magnitude > 0.7f ? WalkFactor.run : WalkFactor.walk );
            InputAxis.Normalize ();

            InputAxis = Vecteur.LDir ( s_camera.o.td.rotY.OnlyY (), InputAxis ) * speed;

            if (sm.state == agc)
            agc.Walk (InputAxis, runFactor );
            else if (sm.state is ac_fall af)
            af.AirMove (InputAxis );
            else if (sm.state is ac_jump aj)
            aj.AirMove (InputAxis );
        }
    }
}