using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_jump : reflexion, IMotorHandler
    {
        [Depend]
        d_ground_data dgd;

        [Depend]
        s_skin_foot_ik ssfi;
        
        [Depend]
        ac_jump aj;

        [Depend]
        ac_fall ac;

        [Depend]
        ac_ground_complex agc;

        [Depend]
        s_motor sm;

        public override void Create()
        {
            aj.Set (4,3);
        }

        public void OnMotorEnd(motor m)
        {
        }

        protected override void Reflex()
        {
            if (dgd.onGround && Player.Jump.OnActive && sm.state != aj)
            {
                aj.jumpAnimation = (agc.state == StateKey.idle)? AnimationKey.jump : ( (ssfi.DominantFoot == s_skin_foot_ik.FootId.left) ? AnimationKey.jump_left_foot : AnimationKey.jump_right_foot );

                ac.landAnimation = (agc.state == StateKey.idle)? AnimationKey.fall_end : ( (ssfi.DominantFoot == s_skin_foot_ik.FootId.left) ? AnimationKey.fall_end_left_foot : AnimationKey.fall_end_right_foot );
                
                sm.SetState (aj,this);
            }

            if (sm.state == aj && Player.Jump.OnRelease)
            {
                aj.StopJump ();
            }
        }
    }
}