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

        protected override void Start()
        {
            
        }

        protected override void Stop()
        {
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
    }
}
