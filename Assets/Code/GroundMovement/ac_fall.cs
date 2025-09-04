using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_fall : motor
    {
        public override int Priority => Rank.Action;
        public override bool AcceptSecondState => true;

        [Link]
        s_capsule_character_controller capsule;
        [Link]
        protected s_gravity_ccc gravitySys;

        [Link]
        protected d_ground_data groundData;
        [Link]
        public s_skin skin;
        [Link]
        s_footstep footstep;

        public static readonly term fall_end = new term ("fall_end");
        static readonly term fall = new term ("fall");
        public term LandKey = fall_end;

        protected override void OnStart()
        {
            this.Link (capsule);
            this.Link (gravitySys);

            var Anim = new SkinAnimation ( fall, this );
            skin.PlayState ( Anim );
        }

        
        protected override void OnStep()
        {
            if (groundData.onGround && gravitySys.Gravity < 0 && Vector3.Angle(Vector3.up, groundData.groundNormal) <= 45)
            {
                skin.PlayState( new SkinAnimation( LandKey, this )
                {
                    LayerIndex = skin.knee,
                    Ev0 = LandSFX
                });
                Stop();
            }
        }

        protected void LandSFX ()
        {
            // footstep.PlayFootstep ();
        }

        public virtual void AirMove ( Vector3 dirPerSecond, float walkFactor = WalkFactor.run )
        {
            if (on)
            capsule.Dir += Time.deltaTime * walkFactor * dirPerSecond;
        }
    }

    public class ac_fall_hard : ac_fall
    {
        public override int Priority => Rank.Action2nd;
        public override bool AcceptSecondState => false;

        bool OnGround;
        static readonly term fall_end_hard = new term ("fall_end_hard");

        protected override void OnStep()
        {
            if ( !OnGround && groundData.onGround && gravitySys.Gravity < 0 && Vector3.Angle(Vector3.up, groundData.groundNormal) <= 45 )
            {
                skin.PlayState (
                    new SkinAnimation ( fall_end_hard, this )
                        {
                            End = HardFallEnd,
                            Ev0 = LandSFX
                        }
                );
                OnGround = true;
            }
        }

        void HardFallEnd()
        {
            Stop ();
        }

        protected override void OnStop()
        {
            base.OnStop ();
            OnGround = false;
        }

        public sealed override void AirMove(Vector3 dirPerSecond, float walkFactor = WalkFactor.run)
        {
            if ( !OnGround )
            base.AirMove ( dirPerSecond,walkFactor );
        }
    }
}
