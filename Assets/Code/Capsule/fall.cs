using Lyra;
using UnityEngine;

namespace Triheroes.Code.CapsuleAct
{
    public class fall : act
    {
        public override priority priority => priority.action.with2nd ();

        [link]
        capsule capsule;
        [link]
        protected capsule.gravity gravity;
        [link]
        protected ground ground;
        [link]
        public skin skin;
        [link]
        footstep footstep;

        public term land_animation = animation.fall_end;

        protected override void _start()
        {
            this.link (capsule);
            this.link (gravity);

            skin.play ( new skin.animation ( animation.fall, this ) );
        }

        
        protected override void _step()
        {
            if (ground && gravity < 0 && Vector3.Angle(Vector3.up, ground.normal) <= 45)
            {
                skin.play( new skin.animation( land_animation, this )
                {
                    layer = skin.knee,
                    ev0 = play_footstep
                });

                stop();
            }
        }

        protected void play_footstep ()
        {
            footstep.play ();
        }
        
        /// <param name="dir">per second</param>
        public virtual void move ( Vector3 dirPerSecond, float walkFactor = walk_factor.run )
        {
            if (on)
            capsule.dir += Time.deltaTime * walkFactor * dirPerSecond;
        }
    }

    public class fall_hard : fall
    {
        public override priority priority => priority.action2.with2nd ();
        bool landed;

        protected override void _step()
        {
            if ( !landed && ground && gravity < 0 && Vector3.Angle(Vector3.up, ground.normal) <= 45 )
            {
                skin.play (
                    new skin.animation ( animation.fall_end_hard, this )
                        {
                            end = stop,
                            ev0 = play_footstep
                        }
                );
                landed = true;
            }
        }

        protected override void _stop()
        {
            base._stop ();
            landed = false;
        }

        /// <param name="dir">per second</param>
        public sealed override void move (Vector3 dir, float factor = walk_factor.run)
        {
            if ( !landed )
            base.move ( dir,factor );
        }
    }
}
