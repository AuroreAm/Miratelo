using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_aim : motor
    {
        // target aim rotation
        float rotY;
        // current rotation x really aimed by the graphic
        float rotX;

        // target rotation
        float TX, TY;
        float AngularDelta => 720 * Time.deltaTime;

        [Depend]
        s_bow_user sbu;
        [Depend]
        s_skin ss;
        [Depend]
        d_ground dg;

        public override int Priority => Pri.SubAction;

        protected override void Start()
        {
            BeginAim ();
        }

        void BeginAim ()
        {
            ss.HoldState ( ss.upper, AnimationKey.begin_aim, .1f );
            Aim (ss.rotY);
        }

        public void Aim(Vector3 Rotation)
        {
            rotY = Rotation.y;
            rotX = Rotation.x;
        }

        protected override void Step()
        {
            TY = Mathf.DeltaAngle ( sbu.Weapon.rotY.y, ss.actualRotY.y ) + rotY;
            dg.rotY.y = Mathf.MoveTowardsAngle (dg.rotY.y,TY, AngularDelta );
            ss.Ani.SetFloat ( Hash.x, Mathf.DeltaAngle ( 0, rotX ) );
        }

        /*
        void Shoot ()
        {
            a_trajectile.Fire ( new term (sbu.Weapon.ArrowName), sbu.Weapon.BowString.position, Quaternion.Euler (rotY), sbu.Weapon.Speed );
        }*/

        protected override void Stop()
        {
            ss.ControlledStop(ss.upper);
        }
    }
}