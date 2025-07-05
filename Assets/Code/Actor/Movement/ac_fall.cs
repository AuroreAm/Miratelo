using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_fall : motor
    {
        public override int Priority => Pri.Action;
        public override bool AcceptSecondState => true;

        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        protected m_gravity_mccc mgm;
        [Depend]
        public m_ground_data mgd;
        [Depend]
        public m_skin ms;
        [Depend]
        m_footstep mf;

        public SuperKey landAnimation = AnimationKey.fall_end;

        protected override void BeginStep()
        {
            ms.PlayState ( 0, AnimationKey.fall, 0.1f );
            mccc.Aquire (this);
            mgm.Aquire (this);
        }

        protected override bool Step()
        {
            if (mgd.onGround && mgm.gravity < 0 && Vector3.Angle(Vector3.up, mgd.groundNormal) <= 45)
            {
                ms.PlayState(ms.knee, landAnimation, 0.05f, null,null, LandSFX);
                return true;
            }
            return false;
        }

        protected void LandSFX ()
        {
            mf.PlayFootstep ();
        }

        protected override void Stop()
        {
            mccc.Free (this);
            mgm.Free (this);
        }

        public virtual void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            mccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }
    }

    public class ac_fall_hard : ac_fall
    {
        public override int Priority => Pri.Action2nd;
        public override bool AcceptSecondState => false;

        bool OnGround;
        protected override bool Step()
        {
            if (!OnGround && mgd.onGround && mgm.gravity < 0 && Vector3.Angle(Vector3.up, mgd.groundNormal) <= 45)
            {
                ms.PlayState(0, AnimationKey.fall_end_hard, 0.1f, HardFallEnd,null, LandSFX);
                OnGround = true;
            }

            return false;
        }

        void HardFallEnd()
        {
            AppendStop ();
        }

        protected override void Stop()
        {
            base.Stop();
            OnGround = false;
        }

        public sealed override void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (!OnGround)
            base.AirMove(DirPerSecond,WalkFactor);
        }
    }
}
