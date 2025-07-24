using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;
using System;

namespace Triheroes.Code
{
    public class ac_dash : motor
    {
        public override int Priority => Pri.Action;

        static term DashAnimation (direction direction) => (direction == direction.forward)? AnimationKey.dash_forward : (direction == direction.right)? AnimationKey.dash_right: (direction == direction.left)? AnimationKey.dash_left:AnimationKey.dash_back;
        public static Vector3 Direction ( direction direction ) => (direction == direction.forward)? Vector3.forward : (direction == direction.back)? Vector3.back:(direction == direction.right)? Vector3.right:Vector3.left;

        [Depend]
        s_capsule_character_controller sccc; int key_ccc;
        [Depend]
        s_skin ss;

        direction DashDirection;
        delta_curve movement;
        term dashAnimation;

        Single TransitionDuration;

        public void SetDashDirection(direction direction)
        {
            DashDirection = direction;
            dashAnimation = DashAnimation(DashDirection);
            TransitionDuration = 0.05f;
        }

        public void OverrideDashAnimation(term animation) => dashAnimation = animation;
        public void OverrideDashAnimation(term animation, Single transitionDuration)
        {
            dashAnimation = animation;
            TransitionDuration = transitionDuration;
        }

        public override void Create()
        {
            movement = new delta_curve(SubResources<CurveRes>.q(new term("jump")).Curve);
        }

        protected override void Start()
        {
            key_ccc = Stage.Start ( sccc );
            ss.PlayState (0, dashAnimation, TransitionDuration, DashEnd);
            movement.Start ( 5, ss.DurationOfState ( dashAnimation ) );
        }

        protected override void Step()
        {
            sccc.dir += Vecteur.LDir(ss.rotY,Direction(DashDirection)) * movement.TickDelta ();
        }

        void DashEnd ()
        {
            SelfStop ();
        }

        protected override void Stop()
        {
            Stage.Stop (key_ccc);
        }
    }
}