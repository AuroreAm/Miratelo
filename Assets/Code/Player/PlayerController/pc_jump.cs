using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [Unique]
    [NodeDescription("transition to: jump")]
    [Category("player controller")]
    public class t_jump_complex : action
    {
        [Depend]
        m_ground_data mgd;
        [Depend]
        m_skin_foot_ik msfi;
        [Depend]
        c_jump cj;
        [Depend]
        c_ground_movement_complex cgmc;
    
        protected override bool Step()
        {
            if (mgd.onGround && Player.GetButtonDown(BoutonId.Jump))
            {
                cj.jumpAnimation = (cgmc.state == StateKey.idle)? AnimationKey.jump : ( (msfi.DominantFoot == m_skin_foot_ik.FootId.left) ? AnimationKey.jump_left_foot : AnimationKey.jump_right_foot );
                cj.landAnimation = (cgmc.state == StateKey.idle)? AnimationKey.fall_end : ( (msfi.DominantFoot == m_skin_foot_ik.FootId.left) ? AnimationKey.fall_end_left_foot : AnimationKey.fall_end_right_foot );
                selector.CurrentSelector.SwitchTo (StateKey2.jump);
            }
            return false;
        }
    }

    [Unique]
    [Category("player controller")]
    public class pc_jump : action
    {
        [Depend]
        c_jump cj;

        // TODO: add blackboard for player speed
        [Export]
        public float speed = 7;

        float jumpTimeHeld;
        bool isJumping;

        protected override void BeginStep()
        {
            cj.Aquire(this);
            cj.JumpOnce ( 3 );
            jumpTimeHeld = 1;
            isJumping = true;
        }

        protected override bool Step()
        {
            Vector3 InputAxis = Player.GetAxis3();
            InputAxis = Vecteur.LDir ( new Vector3 (0,m_camera.o.mct.rotY.y, 0), InputAxis ) * speed;
            cj.AirMove (InputAxis, Player.GetButton(BoutonId.Fire2) ? WalkFactor.sprint : WalkFactor.run);

            if (isJumping)
            {
            jumpTimeHeld -= Time.deltaTime;
            cj.JumpStep (1.5f);
            }

            if (Player.GetButtonUp (BoutonId.Jump) || jumpTimeHeld <= 0)
            isJumping = false;

            return false;
        }

        protected override void Stop()
        {
            cj.Free(this);
        }
    }
}