using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [RegisterAsBase]
    public class c_fall_movement : controller
    {
        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        m_ground_data mgd;
        [Depend]
        m_skin ms;
        [Depend]
        m_footstep mf;

        public SuperKey landAnimation = AnimationKey.fall_end;

        protected override void OnAquire()
        {
            ms.PlayState ( 0, AnimationKey.fall, 0.1f );
            mccc.Aquire(this);
        }

        public void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            mccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }

        public override void Main()
        {
            // to ground movement manually added in behavior trees
        }

        void LandSFX ()
        {
            mf.PlayFootstep ();
        }

        protected override void OnFree()
        {
            if (mgd.onGround && mccc.verticalVelocity < 0 && Vector3.Angle(Vector3.up, mgd.groundNormal) <= 45)
                ms.PlayState(ms.knee, landAnimation, 0.05f, null,null, LandSFX);
            mccc.Free(this);
        }
    }
}
