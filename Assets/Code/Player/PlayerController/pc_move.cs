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
        }

        protected override bool Step()
        {
            Vector3 InputAxis = Player.MoveAxis3;
            float runFactor = Player.Dash.Active ? WalkFactor.sprint : ( InputAxis.magnitude > 0.7f ? WalkFactor.run : WalkFactor.walk );
            InputAxis.Normalize ();

            InputAxis = Vecteur.LDir ( m_camera.o.mct.rotY.OnlyY (), InputAxis ) * speed;
            cgmc.Walk (InputAxis, runFactor );

            return false;
        }

        protected override void Stop()
        {
            cgmc.Free(this);
        }
    }

    /// <summary>
    /// player fall, with built-in transition to: move
    /// </summary>
    [Unique]
    public class pc_fall : action
    {
        [Depend]
        c_air_movement cam;
        [Depend]
        m_skin ms;
        [Depend]
        m_footstep mf;

        [Export]
        public float speed = 7;

        float time;
        bool hardFall;
        bool Stopping;
        bool Done;
        

        public SuperKey landAnimation = AnimationKey.fall_end;

        protected override void BeginStep()
        {
            time = 0;
            hardFall = false;
            cam.Aquire(this);
        }

        protected override bool Step()
        {
            if (Stopping && Done)
            {
                Done = false;
                Stopping = false;

                selector.CurrentSelector.SwitchTo (StateKey2.move);
            }

            if (Stopping) return false;

            Vector3 InputAxis = Player.MoveAxis3;
            InputAxis = Vecteur.LDir ( new Vector3 (0,m_camera.o.mct.rotY.y, 0), InputAxis ) * speed;
            cam.AirMove (InputAxis);

            if (!hardFall)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    hardFall = true;
                    time = 0;
                }
            }
            
            if (cam.mgd.onGround && cam.mccc.verticalVelocity < 0 && Vector3.Angle(Vector3.up, cam.mgd.groundNormal) <= 45)
            {
                if (hardFall)
                {
                    ms.PlayState(0, AnimationKey.fall_end_hard, 0.1f, HardFallEnd,null, LandSFX);
                    Stopping = true;
                }
                else
                {
                    ms.PlayState(ms.knee, landAnimation, 0.05f, null,null, LandSFX);
                    selector.CurrentSelector.SwitchTo (StateKey2.move);
                }
            }

            return false;
        }
        
        void HardFallEnd()
        {
            Done = true;
        }

        void LandSFX ()
        {
            mf.PlayFootstep ();
        }

        protected override void Stop()
        {
            cam.Free(this);
        }
    }

}