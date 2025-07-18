using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_aim : controller
    {
        [Depend]
        s_bow_user sbu;
        [Depend]
        s_skin ss;

        [Depend]
        sp_bow sb; int key_b;

        protected override void Start()
        {
            key_b = Stage.Start (sb);
            BeginAim ();
        }

        void BeginAim ()
        {
            ss.HoldState ( ss.upper, AnimationKey.begin_aim, .1f );

            sb.FollowTargetRotation = true;
            Aim (ss.rotY);
        }

        public void Aim ( Vector3 Rotation )
        {
            sbu.rotY = Rotation;
            sb.TargetRotation = Rotation;
        }

        public void StartShoot ()
        {
            if (on)
            {
                ss.PlayState ( ss.upper, AnimationKey.start_shoot, 0.1f, null, null, Shoot );
            }
        }

        void Shoot ()
        {
            a_trajectile.Fire ( new term (sbu.Weapon.ArrowName), sbu.Weapon.BowString.position, Quaternion.Euler (sbu.rotY), sbu.Weapon.Speed );
        }

        protected override void Stop()
        {
            sb.FollowTargetRotation = false;
            ss.ControlledStop ( ss.upper );

            Stage.Stop (key_b);
        }
    }
}