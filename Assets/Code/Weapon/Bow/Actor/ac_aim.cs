using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ac_aim : motor
    {
        // target aim rotation
        float rotY, rotX;
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
            Aim (0,ss.rotY);
        }

        public void Aim(float x, float y)
        {
            rotY = y;
            rotX = x;
        }

        public void Shot ()
        {
            a_trajectile.Fire ( new term (sbu.Weapon.ArrowName), sbu.Weapon.BowString.position, Quaternion.Euler ( new Vector3 (rotX, rotY) ), sbu.Weapon.Speed );
        }

        protected override void Step()
        {
            float TY = Mathf.DeltaAngle ( sbu.Weapon.rotY.y, ss.actualRotY ) + rotY;
            dg.rotY = Mathf.MoveTowardsAngle (dg.rotY,TY, AngularDelta );
            ss.Ani.SetFloat ( Hash.x, Mathf.DeltaAngle ( 0, rotX ) );
        }
        
        protected override void Stop()
        {
            ss.ControlledStop(ss.upper);
        }
    }
}