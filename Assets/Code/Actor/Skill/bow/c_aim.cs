using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
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

        public void StartShoot ()
        {
            if (on)
            {
                ms.PlayState ( ms.upper, AnimationKey.start_shoot, 0.1f, null, null, Shoot );
            }
        }

        void Shoot ()
        {
            p_trajectile.Fire ( new SuperKey (mbu.Weapon.ArrowName), mbu.Weapon.BowString.position, Quaternion.Euler (mbu.rotY), mbu.Weapon.Speed );
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

    
    public class ac_aiming : action
    {
        [Depend]
        c_aim ca;

        protected override bool Step()
        {
            return !ca.integral.aquired;
        }
    }
}