using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_jump : motor
    {
        public override int Priority => Pri.Action;
        public override bool AcceptSecondState => true;

        [Depend]
        public s_capsule_character_controller sccc; int key_ccc;
        [Depend]
        s_skin ss;

        delta_curve cu;
        float jumpHeight;
        float minimumJumpHeight;
        bool done;

        public term jumpAnimation = AnimationKey.jump;

        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new term ("jump") ).Curve );
        }

        public void Set (float jumpHeight, float minimumJumpHeight)
        {
            this.jumpHeight = jumpHeight;
            this.minimumJumpHeight = minimumJumpHeight;
        }

        protected override void Start()
        {
            key_ccc = Stage.Start ( sccc );
            cu.Start ( jumpHeight, .5f );
            ss.PlayState(0, jumpAnimation, 0.1f);
            done = false;
        }

        protected override void Stop()
        {
            Stage.Stop ( key_ccc );
            done = false;
        }

        public void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            sccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }

        public void StopJump ()
        {
            done = true;
        }

        protected override void Step()
        {
            sccc.dir += new Vector3 ( 0, cu.TickDelta() , 0 );
            
            if ( done && cu.currentValue >= minimumJumpHeight )
            {
                SelfStop ();
                return;
            }

            if (cu.Done)
            SelfStop ();
        }
    }
}
