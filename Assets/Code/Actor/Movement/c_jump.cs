using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [RegisterAsBase]
    public class c_jump : controller
    {
        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        m_skin ms;
        [Depend]
        c_fall_movement cfm;

        public SuperKey jumpAnimation = AnimationKey.jump;
        public SuperKey landAnimation = AnimationKey.fall_end;

        protected override void OnAquire()
        {
            mccc.Aquire(this);
        }

        protected override void OnFree()
        {
            cfm.landAnimation = landAnimation;
            mccc.Free(this);
        }

        public void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            mccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }

        public void JumpOnce ( float JumpHeight )
        {
            if (on)
            {
                mccc.verticalVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * JumpHeight * mccc.mass);
                ms.PlayState(0, jumpAnimation, 0.1f);
            }
        }

        public void JumpStep ( float UnpreciseRatio )
        {
            if (on)
            {
                mccc.verticalVelocity += Mathf.Sqrt(-2f * Physics.gravity.y * UnpreciseRatio * mccc.mass) * Time.deltaTime;
            }
        }

        public override void Main() {}
    }
}
