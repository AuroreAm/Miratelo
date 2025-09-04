using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_jump : motor
    {
        public override int priority => Rank.Action;
        public override bool accept2nd => true;

        [harmony]
        s_capsule_character_controller capsule;
        [harmony]
        s_skin skin;

        delta_curve cu;
        float jumpHeight;
        float minimumJumpHeight;
        bool done;

        public static readonly term jump = new term ( "jump" );
        public term JumpKey = jump;

        protected override void harmony ()
        {
            cu = new delta_curve( TriheroesRes.Curve.Q ( jump ).Curve );
        }

        public void Set ( float jumpHeight, float minimumJumpHeight )
        {
            this.jumpHeight = jumpHeight;
            this.minimumJumpHeight = minimumJumpHeight;
        }

        protected override void awaken()
        {
            this.link(capsule);

            cu.Start(jumpHeight, .5f);
            skin.PlayState( new SkinAnimation ( JumpKey, this ) );
            done = false;
        }
        
        public void AirMove(Vector3 DirPerSecond, float WalkFactor = WalkFactor.run)
        {
            if (on)
                capsule.Dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }

        public void StopJump()
        {
            if ( cu.CurrentValue >= minimumJumpHeight && cu.CurrentValue < (jumpHeight + minimumJumpHeight)/ 2 )
            done = true;
        }

        protected override void alive()
        {
            capsule.Dir += new Vector3(0, cu.TickDelta (), 0);

            if ( done )
            {
                sleep ();
                return;
            }

            if (cu.Done)
                sleep();
        }
    }
}
