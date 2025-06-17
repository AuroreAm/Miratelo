using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    /// <summary>
    /// player normal movement on ground
    /// </summary>
    [Unique]
    public class pc_move : action
    {
        [Depend]
        c_ground_movement_complex cgmc;

        public float speed = 7;

        protected override void BeginStep()
        {
            cgmc.Aquire(this);

            // Test
            trail = s_trail_spectre.Bind (new SuperKey("sp_trail"));
        }

        protected override bool Step()
        {
            Vector3 InputAxis = Player.GetAxis3();
            InputAxis = Vecteur.LDir ( m_camera.o.mct.rotY.OnlyY (), InputAxis ) * speed;
            // TODO: add buttonID for walking
            cgmc.Walk (InputAxis, Player.GetButton(BoutonId.Fire2) ? WalkFactor.sprint : Input.GetKey(KeyCode.X)? WalkFactor.walk : WalkFactor.run);

            // test
            s_trail_spectre.AddPosition (trail, cgmc.character.transform.position);

            return false;
        }

        int trail;

        protected override void Stop()
        {
            cgmc.Free(this);
        }
    }

    /// <summary>
    /// player controller
    /// </summary>
    [Unique]
    public class pc_fall : action
    {
        [Depend]
        c_fall_movement cfm;
        
        [Export]
        public float speed = 7;

        protected override void BeginStep()
        {
            cfm.Aquire(this);
        }

        protected override bool Step()
        {
            Vector3 InputAxis = Player.GetAxis3();
            InputAxis = Vecteur.LDir ( new Vector3 (0,m_camera.o.mct.rotY.y, 0), InputAxis ) * speed;
            cfm.AirMove (InputAxis);
            return false;
        }

        protected override void Stop()
        {
            cfm.Free(this);
        }

    }

}