using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_jump : reflection
    {
        [Depend]
        m_ground_data mgd;

        [Depend]
        m_skin_foot_ik msfi;
        
        [Depend]
        ac_jump aj;

        [Depend]
        ac_fall ac;

        [Depend]
        ac_ground_complex agc;

        public override void Create()
        {
            aj.Set (4,3);
        }

        public override void Main()
        {
            if (mgd.onGround && Player.Jump.OnActive && mst.state != aj)
            {
                aj.jumpAnimation = (agc.state == StateKey.idle)? AnimationKey.jump : ( (msfi.DominantFoot == m_skin_foot_ik.FootId.left) ? AnimationKey.jump_left_foot : AnimationKey.jump_right_foot );

                ac.landAnimation = (agc.state == StateKey.idle)? AnimationKey.fall_end : ( (msfi.DominantFoot == m_skin_foot_ik.FootId.left) ? AnimationKey.fall_end_left_foot : AnimationKey.fall_end_right_foot );
                
                mst.SetState (aj,Pri.Action,true);
            }

            if (mst.state == aj && Player.Jump.OnRelease)
            {
                aj.StopJump ();
            }
        }
    }
}