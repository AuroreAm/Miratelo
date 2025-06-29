using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class ac_dash : action
    {
        static SuperKey DashAnimation (direction direction) => (direction == direction.forward)? AnimationKey.dash_forward : (direction == direction.right)? AnimationKey.dash_right:AnimationKey.dash_left;
        public static Vector3 Direction ( direction direction ) => (direction == direction.forward)? Vector3.forward : (direction == direction.back)? Vector3.back:(direction == direction.right)? Vector3.right:Vector3.left;

        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        m_skin ms;

        public direction DashDirection;

        float JumpHeight = 1.5f;
        delta_curve cu;
        public override void Create()
        {
            cu = new delta_curve ( SubResources <CurveRes>.q ( new SuperKey ("jump") ).Curve );
        }

        protected override void BeginStep()
        {
            mccc.Aquire (this);

            if ( DashDirection == direction.back )
            BackFlip ();
            else
            ms.PlayState (0, DashAnimation (DashDirection), 0.05f, DashEnd);
            ms.allowMoving = true;
            ms.SkinDir = Vecteur.LDir(ms.rotY,Direction(DashDirection));
        }

        protected override bool Step()
        {
            if (DashDirection == direction.back)
            mccc.dir += new Vector3 ( 0, cu.TickDelta() , 0 );
            return false;
        }

        void BackFlip ()
        {
            cu.Start ( JumpHeight, .4f );
            ms.PlayState (0, AnimationKey.dash_back, 0.05f, DashEnd);
        }

        void DashEnd ()
        {
            AppendStop ();
        }

        protected override void Stop()
        {
            ms.allowMoving = false;
            mccc.Free (this);
        }
    }
}