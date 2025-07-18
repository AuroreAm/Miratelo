using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class ac_dash : motor
    {
        public override int Priority => Pri.Action;

        static term DashAnimation (direction direction) => (direction == direction.forward)? AnimationKey.dash_forward : (direction == direction.right)? AnimationKey.dash_right:AnimationKey.dash_left;
        public static Vector3 Direction ( direction direction ) => (direction == direction.forward)? Vector3.forward : (direction == direction.back)? Vector3.back:(direction == direction.right)? Vector3.right:Vector3.left;

        [Depend]
        s_capsule_character_controller sccc; int key_ccc;
        [Depend]
        s_skin ss;

        public direction DashDirection;

        float JumpHeight = 1.5f;
        delta_curve cu;
        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new term ("jump") ).Curve );
        }

        protected override void Start()
        {
            key_ccc = Stage.Start ( sccc );

            if ( DashDirection == direction.back )
            BackFlip ();
            else
            ss.PlayState (0, DashAnimation (DashDirection), 0.05f, DashEnd);
            ss.allowMoving = true;
            ss.SkinDir = Vecteur.LDir(ss.rotY,Direction(DashDirection));
        }

        protected override void Step()
        {
            if (DashDirection == direction.back)
            sccc.dir += new Vector3 ( 0, cu.TickDelta() , 0 );
        }

        void BackFlip ()
        {
            cu.Start ( JumpHeight, .4f );
            ss.PlayState (0, AnimationKey.dash_back, 0.05f, DashEnd);
        }

        void DashEnd ()
        {
            SelfStop ();
        }

        protected override void Stop()
        {
            ss.allowMoving = false;
            Stage.Stop (key_ccc);
        }
    }
}