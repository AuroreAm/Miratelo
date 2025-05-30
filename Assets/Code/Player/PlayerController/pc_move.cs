using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Category("player controller")]
    public class pc_move : action
    {
        [Depend]
        c_ground_movement_complex cgmc;

        [Export]
        public float speed = 7;

        protected override void BeginStep()
        {
            cgmc.Aquire(this);
        }

        protected override bool Step()
        {
            Vector3 InputAxis = Player.GetAxis3();
            InputAxis = Vecteur.LDir ( new Vector3 (0,m_camera.o.mct.rotY.y, 0), InputAxis ) * speed;
            // TODO: add buttonID for walking
            cgmc.Walk (InputAxis, Player.GetButton(BoutonId.Fire2) ? WalkFactor.sprint : Input.GetKey(KeyCode.X)? WalkFactor.walk : WalkFactor.run);
            return false;
        }

        protected override void Stop()
        {
            cgmc.Free(this);
        }
    }

    [Category("player controller")]
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