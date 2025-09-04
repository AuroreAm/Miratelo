using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_fall : motor
    {
        public override int priority => Rank.Action;
        public override bool accept2nd => true;

        [harmony]
        s_capsule_character_controller capsule;
        [harmony]
        protected s_gravity_ccc gravitySys;

        [harmony]
        protected d_ground_data groundData;
        [harmony]
        public s_skin skin;
        [harmony]
        s_footstep footstep;

        public static readonly term fall_end = new term ("fall_end");
        static readonly term fall = new term ("fall");
        public term LandKey = fall_end;

        protected override void awaken()
        {
            this.link (capsule);
            this.link (gravitySys);

            var Anim = new SkinAnimation ( fall, this );
            skin.PlayState ( Anim );
        }

        
        protected override void alive()
        {
            if (groundData.onGround && gravitySys.Gravity < 0 && Vector3.Angle(Vector3.up, groundData.groundNormal) <= 45)
            {
                skin.PlayState( new SkinAnimation( LandKey, this )
                {
                    LayerIndex = skin.knee,
                    Ev0 = LandSFX
                });
                sleep();
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
        public override int priority => Rank.Action2nd;
        public override bool accept2nd => false;

        bool OnGround;
        static readonly term fall_end_hard = new term ("fall_end_hard");

        protected override void alive()
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
            sleep ();
        }

        protected override void asleep()
        {
            base.asleep ();
            OnGround = false;
        }

        public sealed override void AirMove(Vector3 dirPerSecond, float walkFactor = WalkFactor.run)
        {
            if ( !OnGround )
            base.AirMove ( dirPerSecond,walkFactor );
        }
    }
}
