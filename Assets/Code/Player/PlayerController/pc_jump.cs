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
        c_jump cj;

        [Depend]
        ac_fall ac;

        [Depend]
        pc_jump pj;

        [Depend]
        ac_ground_complex agc;

        public override void Main()
        {
            if (mgd.onGround && Player.Jump.OnActive && mst.state != pj)
            {
                cj.jumpAnimation = (agc.state == StateKey.idle)? AnimationKey.jump : ( (msfi.DominantFoot == m_skin_foot_ik.FootId.left) ? AnimationKey.jump_left_foot : AnimationKey.jump_right_foot );

                ac.landAnimation = (agc.state == StateKey.idle)? AnimationKey.fall_end : ( (msfi.DominantFoot == m_skin_foot_ik.FootId.left) ? AnimationKey.fall_end_left_foot : AnimationKey.fall_end_right_foot );
                
                mst.SetState (pj,Pri.Action,true);
            }
        }
    }

    public class pc_jump : action
    {
        [Depend]
        c_jump cj;

        // TODO: add blackboard for player speed
        [Export]
        public float speed = 7;

        float jumpTimeHeld;

        protected override void BeginStep()
        {
            cj.Aquire(this);
            cj.JumpOnce ( 3 );
            jumpTimeHeld = 1;
        }

        protected override bool Step()
        {
            Vector3 InputAxis = Player.MoveAxis3;
            InputAxis = Vecteur.LDir ( new Vector3 (0,m_camera.o.td.rotY.y, 0), InputAxis ) * speed;
            cj.AirMove (InputAxis, Player.Jump.Active? WalkFactor.sprint : WalkFactor.run);

            jumpTimeHeld -= Time.deltaTime;
            cj.JumpStep (1.5f);

            if ( cj.mccc.verticalVelocity < 0 || jumpTimeHeld <= 0)
            return true;

            return false;
        }

        protected override void Stop()
        {
            cj.Free(this);
        }
    }
}