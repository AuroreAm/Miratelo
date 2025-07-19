using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pc_lateral_move : action , IMotorHandler
    {
        [Depend]
        s_motor sm;

        [Depend]
        ac_ground_movement_lateral agml;

        public float speed = 6;

        public void OnMotorEnd(motor m)
        {}

        protected override void Step()
        {
            if ( !agml.on )
            sm.SetState ( agml, this );

            Vector3 InputAxis;
            InputAxis = Player.MoveAxis3;
            InputAxis = Vecteur.LDir ( s_camera.o.td.rotY.OnlyY (),InputAxis) * 6f;

            agml.WalkLateral ( InputAxis );
        }

        protected override void Stop()
        {
            if ( sm.state == agml )
            sm.EndState ( this );
        }
    }
}
