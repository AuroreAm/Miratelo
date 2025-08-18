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
        public static Vector3 DirectionDir ( direction direction ) => (direction == direction.forward)? Vector3.forward : (direction == direction.back)? Vector3.back:(direction == direction.right)? Vector3.right:Vector3.left;

        [Depend]
        s_capsule_character_controller sccc;
        [Depend]
        s_skin ss;
        [Depend]
        d_ground dg;

        public Vector3 directionDir;
        direction DashDirection;
        delta_curve movement;
        term dashAnimation;

        float TransitionDuration;

        public void SetDashDirection(direction direction)
        {
            DashDirection = direction;
            dashAnimation = DashAnimation(DashDirection);
            directionDir = DirectionDir(DashDirection);
            TransitionDuration = 0.05f;
        }

        public void OverrideDashAnimation(term animation) => dashAnimation = animation;
        public void OverrideDashAnimation(term animation, float transitionDuration)
        {
            dashAnimation = animation;
            TransitionDuration = transitionDuration;
        }

        public override void Create()
        {
            Link (sccc);

            movement = new delta_curve(SubResources<CurveRes>.q(new term("jump")).Curve);
        }

        protected override void Start()
        {
            ss.PlayState (0, dashAnimation, TransitionDuration, DashEnd);
            movement.Start ( 5, ss.DurationOfState ( dashAnimation ) );
        }

        protected override void Step()
        {
            sccc.dir += Vecteur.LDir(ss.rotY,directionDir) * movement.TickDelta ();
            dg.PerformSkillRotation ();
        }

        void DashEnd ()
        {
            SelfStop ();
        }
    }
}