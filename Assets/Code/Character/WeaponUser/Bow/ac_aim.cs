using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_aim : motor
    {
        [Depend]
        s_bow_user sbu;
        [Depend]
        s_skin ss;

        [Depend]
        sp_bow sb;

        public override int Priority => Pri.SubAction;

        public override void Create()
        {
            Link (sb);
        }

        protected override void Start()
        {
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
        }
    }
}