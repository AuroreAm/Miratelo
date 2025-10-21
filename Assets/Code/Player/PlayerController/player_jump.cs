using Lyra;
using static Triheroes.Code.anim;

namespace Triheroes.Code
{
    [ path ("player controller") ]
    public class player_jump : action
    {
        [link]
        Axeal.ground ground;
        [link]
        foot_ik ik;
        [link]
        fall fall;
        [link]
        move move;
        [link]
        motor motor;

        [link]
        jump jump;

        buffer iground = new buffer ( .3f );
        buffer ijump = new buffer ( .25f );
        buffer istop = new buffer ( .25f );

        protected override void _ready() {
            jump.set (2);
        }

        protected override void _step ()
        {
            if ( player.S.down )
                ijump.stack ();

            if ( ground )
            iground.stack ();

            if ( jump_down () )
                start_jump ();

            if ( ( ijump.on || motor.act == jump ) && player.S.up )
            istop.stack ();

            if ( istop.on ) {
                jump.stop_jump ();
                istop.clear ();
            }
        }

        public bool jump_down () {
            return iground.on && ijump.on && motor.act != jump;
        }

        public void start_jump () {
            jump.jump_animation = (move.state == idle)? anim.jump : ( (ik.dominand_foot == foot_ik.foot.left) ? jump_left_foot : jump_right_foot );

            fall.land_animation = (move.state == idle)? fall_end : ( (ik.dominand_foot == foot_ik.foot.left) ? fall_end_left_foot : fall_end_right_foot );
            
            bool success;
            success = motor.start_act (jump);

            if ( success ) {
            ijump.clear (); iground.clear ();
            }
        }

        public bool active () {
            return motor.act == jump;
        }
    }
}
