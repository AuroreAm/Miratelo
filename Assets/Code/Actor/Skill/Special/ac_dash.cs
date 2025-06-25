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

        void BackFlip ()
        {
            const float JumpHeight = 1.75f;
            mccc.verticalVelocity = Mathf.Sqrt ( JumpHeight * -2f * Physics.gravity.y * mccc.mass );
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