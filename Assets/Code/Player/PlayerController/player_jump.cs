using Lyra;
using Triheroes.Code.CapsuleAct;
using static Triheroes.Code.animation;

namespace Triheroes.Code
{
    [ path ("player controller") ]
    public class player_jump : action
    {
        [link]
        ground ground;
        [link]
        foot_ik ik;
        [link]
        jump jump;
        [link]
        fall fall;
        [link]
        move move;
        [link]
        motor motor;

        protected override void _ready()
        {
            jump.set (4,3);
        }

        protected override void _step ()
        {
            if (ground && player.jump.down && motor.act != jump)
            {
                jump.jump_animation = (move.state == idle)? animation.jump : ( (ik.dominand_foot == foot_ik.foot.left) ? jump_left_foot : jump_right_foot );

                fall.land_animation = (move.state == idle)? fall_end : ( (ik.dominand_foot == foot_ik.foot.left) ? fall_end_left_foot : fall_end_right_foot );
                
                motor.start_act (jump);
            }

            if (motor.act == jump && player.jump.up)
                jump.stop_jump ();
        }
    }
}
