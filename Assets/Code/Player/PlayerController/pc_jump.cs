using Lyra;

namespace Triheroes.Code
{

    [ Path ("player controller") ]
    public class pc_jump : action, IMotorHandler
    {
        [Link]
        d_ground_data groundData;

        [Link]
        s_skin_foot_ik footIK;
        
        [Link]
        ac_jump acJump;

        [Link]
        ac_fall acFall;

        [Link]
        ac_ground_complex acGroundMove;

        [Link]
        s_motor motor;

        protected override void OnStructured()
        {
            acJump.Set (4,3);
        }

        public void OnMotorEnd(motor m)
        {}

        protected override void OnStep ()
        {
            if (groundData.onGround && Player.Jump.OnActive && motor.State != acJump)
            {
                acJump.JumpKey = (acGroundMove.state == ac_ground_complex.idle)? ac_jump.jump : ( (footIK.DominantFoot == s_skin_foot_ik.FootId.left) ? jump_left_foot : jump_right_foot );

                acFall.LandKey = (acGroundMove.state == ac_ground_complex.idle)? ac_fall.fall_end : ( (footIK.DominantFoot == s_skin_foot_ik.FootId.left) ? fall_end_left_foot : fall_end_right_foot );
                
                motor.SetState (acJump,this);
            }

            if (motor.State == acJump && Player.Jump.OnRelease)
                acJump.StopJump ();
        }

        public static term jump_left_foot = new term ("jump_left_foot");
        public static term jump_right_foot = new term ("jump_right_foot");
        public static term fall_end_right_foot = new term ("fall_end_right_foot");
        public static term fall_end_left_foot = new term ("fall_end_left_foot");
    }
}
