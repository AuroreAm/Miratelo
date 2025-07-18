using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// player lateral move on ground
    /// </summary>
    /// // INPROGRESS
    public class pc_lateral_move : action
    {
        [Depend]
        ac_ground_movement_lateral cgml;
        int key_gml;

        public float speed = 6;

        protected override void Start ()
        {
            key_gml = Stage.Start (cgml);
        }

        protected override void Step()
        {
            Vector3 InputAxis;
            InputAxis = Player.MoveAxis3;
            InputAxis = Vecteur.LDir ( s_camera.o.td.rotY.OnlyY (),InputAxis) * 6f;

            cgml.WalkLateral ( InputAxis );
        }

        protected override void Stop()
        {
            Stage.Stop ( key_gml );
        }

    }
}
