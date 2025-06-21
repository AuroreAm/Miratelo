using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// player lateral move on ground
    /// </summary>
    public class pc_lateral_move : action
    {
        [Depend]
        c_ground_movement_lateral cgml;

        public float speed = 6;

        protected override void BeginStep()
        {
            cgml.Aquire (this);
        }

        protected override bool Step()
        {
            Vector3 InputAxis;
            InputAxis = Player.MoveAxis3;
            InputAxis = Vecteur.LDir ( m_camera.o.td.rotY.OnlyY (),InputAxis) * 6f;

            cgml.WalkLateral ( InputAxis );

            return false;
        }

        protected override void Stop()
        {
            cgml.Free (this);
        }

    }
}
