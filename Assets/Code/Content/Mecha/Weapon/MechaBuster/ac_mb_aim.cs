using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    // NOTE same as ac_aim with few difference
    // TODO refactor
    public class ac_mb_aim : motor
    {
        float rotY;
        float AngularDelta => 720 * Time.deltaTime;

        [Depend]
        s_skin ss;
        [Depend]
        d_ground dg;
        [Depend]
        d_mecha_buster dmb;

        public override int Priority => Pri.SubAction;

        protected override void Start()
        {
            BeginAim ();
        }

        void BeginAim ()
        {
            ss.HoldState ( ss.upper, AnimationKey.begin_aim, .1f );
            Aim (ss.rotY.y);
        }

        public void Aim(float Y)
        {
            rotY = Y;
        }

        protected override void Step()
        {
            float TY = Mathf.DeltaAngle ( dmb.rotY, ss.actualRotY.y ) + rotY;
            dg.rotY.y = Mathf.MoveTowardsAngle (dg.rotY.y,TY, AngularDelta );
        }
        
        protected override void Stop()
        {
            ss.ControlledStop(ss.upper);
        }
    }
}