using Lyra;

namespace Triheroes.Code
{

    [ verse ("player controller") ]
    public class pc_jump : act, ILucid
    {
        [harmony]
        d_ground_data groundData;

        [harmony]
        s_skin_foot_ik footIK;
        
        [harmony]
        ac_jump acJump;

        [harmony]
        ac_fall acFall;

        [harmony]
        ac_ground_complex acGroundMove;

        [harmony]
        kinesis motor;

        protected override void harmony()
        {
            acJump.Set (4,3);
        }

        public void inhalt(motor m)
        {}

        protected override void alive ()
        {
            if (groundData.onGround && Player.Jump.OnActive && motor.state != acJump)
            {
                acJump.JumpKey = (acGroundMove.state == ac_ground_complex.idle)? ac_jump.jump : ( (footIK.DominantFoot == s_skin_foot_ik.FootId.left) ? jump_left_foot : jump_right_foot );

                acFall.LandKey = (acGroundMove.state == ac_ground_complex.idle)? ac_fall.fall_end : ( (footIK.DominantFoot == s_skin_foot_ik.FootId.left) ? fall_end_left_foot : fall_end_right_foot );
                
                motor.perform (acJump,this);
            }

            if (motor.state == acJump && Player.Jump.OnRelease)
                acJump.StopJump ();
        }

        public static term jump_left_foot = new term ("jump_left_foot");
        public static term jump_right_foot = new term ("jump_right_foot");
        public static term fall_end_right_foot = new term ("fall_end_right_foot");
        public static term fall_end_left_foot = new term ("fall_end_left_foot");
    }
}
