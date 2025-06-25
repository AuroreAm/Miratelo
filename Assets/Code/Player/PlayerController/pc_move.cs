using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_move : reflection
    {
        [Depend]
        ac_ground_complex agc;

        float speed = 7;
        public override void Main()
        {
            if (mst.state == null)
                mst.SetState (agc,Pri.def,true);

            Vector3 InputAxis = Player.MoveAxis3;
            float runFactor = Player.Dash.Active ? WalkFactor.sprint : ( InputAxis.magnitude > 0.7f ? WalkFactor.run : WalkFactor.walk );
            InputAxis.Normalize ();

            InputAxis = Vecteur.LDir ( m_camera.o.td.rotY.OnlyY (), InputAxis ) * speed;

            if (mst.state == agc)
            agc.Walk (InputAxis, runFactor );
            else if (mst.state is ac_fall af)
            af.AirMove (InputAxis );
        }
    }

}