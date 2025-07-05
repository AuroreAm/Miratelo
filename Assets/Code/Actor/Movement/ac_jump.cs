using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_jump : motor
    {
        public override int Priority => Pri.Action;
        public override bool AcceptSecondState => true;

        [Depend]
        public m_capsule_character_controller mccc;
        [Depend]
        m_skin ms;

        delta_curve cu;
        float jumpHeight;
        float minimumJumpHeight;
        bool done;

        public SuperKey jumpAnimation = AnimationKey.jump;

        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new SuperKey ("jump") ).Curve );
        }

        public void Set (float jumpHeight, float minimumJumpHeight)
        {
            this.jumpHeight = jumpHeight;
            this.minimumJumpHeight = minimumJumpHeight;
        }

        protected override void BeginStep()
        {
            mccc.Aquire ( this );
            cu.Start ( jumpHeight, .5f );
            ms.PlayState(0, jumpAnimation, 0.1f);
            done = false;
        }

        protected override void Stop()
        {
            mccc.Free ( this );
            done = false;
        }

        public void AirMove(Vector3 DirPerSecond,float WalkFactor = WalkFactor.run)
        {
            if (on)
            mccc.dir += DirPerSecond * Time.deltaTime * WalkFactor;
        }

        public void StopJump ()
        {
            done = true;
        }

        protected override bool Step()
        {
            mccc.dir += new Vector3 ( 0, cu.TickDelta() , 0 );
            
            if ( done && cu.currentValue >= minimumJumpHeight )
            return true;

            return cu.Done;
        }
    }
}
