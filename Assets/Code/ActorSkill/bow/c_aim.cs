using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [RegisterAsBase]
    public class c_aim : controller
    {
        [Depend]
        m_bow_user mbu;
        [Depend]
        m_skin ms;
        [Depend]
        msp_bow mb;

        protected override void OnAquire()
        {
            mb.Aquire (this);
            BeginAim ();
        }

        void BeginAim ()
        {
            ms.HoldState ( ms.upper, AnimationKey.begin_aim, .1f );

            mb.FollowTargetRotation = true;
            Aim (ms.rotY);
        }

        public void Aim ( Vector3 Rotation )
        {
            mbu.rotY = Rotation;
            mb.TargetRotation = Rotation;
        }

        protected override void OnFree()
        {
            mb.FollowTargetRotation = false;
            ms.ControlledStop ( ms.upper );

            mb.Free (this);
        }

        public override void Main()
        {}
    }
}