using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class ac_mb_aim : motor
    {
        public override int Priority => Pri.SubAction;
        
        [Depend]
        s_mecha_buster_user smbu;
        
        [Depend]
        s_skin ss;

        [Depend]
        sp_mb smb;

        public override void Create()
        {
            Link ( smb );
        }

        protected override void Start()
        {
            ss.HoldState ( 0, AnimationKey.begin_aim, .1f );
        }

        public void Aim ( Vector3 Rotation )
        {
            smbu.rotY = Rotation;
        }
    }

    public class sp_mb : s_skin_procedural
    {
        [Depend]
        s_mecha_buster_user smbu;

        // transform
        Transform Spine;
        Transform ArmBuster;

        public class package : PreBlock.Package <sp_mb>
        {
            public package ( Transform spine, Transform armBuster )
            {
                o.Spine = spine;
                o.ArmBuster = armBuster;
            }
        }

        protected override void Step()
        {
            if ( Input.GetKey (KeyCode.A) )
            smbu.rotY.y += 1f;
            
            if ( Input.GetKey (KeyCode.D) )
            smbu.rotY.y -= 1f;

            if ( Input.GetKey (KeyCode.W) )
            smbu.rotY.x += 1f;

            if ( Input.GetKey (KeyCode.S) )
            smbu.rotY.x -= 1f;

            ArmBuster.Rotate ( smbu.rotY.x, 0, 0, Space.World );
            Spine.Rotate ( 0, smbu.rotY.y, 0, Space.World );
        }
    }
}
