using System.Collections;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_fall : motor
    {
        public override int Priority => Pri.Action;
        public override bool AcceptSecondState => true;

        [Depend]
        s_capsule_character_controller sccc; int key_ccc;
        [Depend]
        protected s_gravity_ccc sgc; int key_gc;

        [Depend]
        public d_ground_data dgd;
        [Depend]
        public s_skin ss;
        [Depend]
        s_footstep sf;

        public term landAnimation = AnimationKey.fall_end;

        protected override void Start()
        {
            ss.PlayState ( 0, AnimationKey.fall, 0.1f );
            key_ccc = Stage.Start ( sccc );
            key_gc = Stage.Start ( sgc );
        }

        protected override void Step()
        {
            if (dgd.onGround && sgc.gravity < 0 && Vector3.Angle(Vector3.up, dgd.groundNormal) <= 45)
            {
                ss.PlayState(ss.knee, landAnimation, 0.05f, null,null, LandSFX);
                SelfStop ();
            }
        }

        protected void LandSFX ()
        {
            sf.PlayFootstep ();
        }

        protected override void Stop()
        {
            Stage.Stop ( key_ccc );
            Stage.Stop ( key_gc );
        }

        public virtual void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            sccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }
    }

    public class ac_fall_hard : ac_fall
    {
        public override int Priority => Pri.Action2nd;
        public override bool AcceptSecondState => false;

        bool OnGround;
        protected override void Step()
        {
            if (!OnGround && dgd.onGround && sgc.gravity < 0 && Vector3.Angle(Vector3.up, dgd.groundNormal) <= 45)
            {
                ss.PlayState(0, AnimationKey.fall_end_hard, 0.1f, HardFallEnd,null, LandSFX);
                OnGround = true;
            }
        }

        void HardFallEnd()
        {
            SelfStop ();
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
