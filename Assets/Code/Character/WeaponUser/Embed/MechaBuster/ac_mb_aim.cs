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

        protected override void Start()
        {
            ss.HoldState ( 0, AnimationKey.begin_aim, .1f );
        }

        public void Aim ( Vector3 Rotation )
        {
            smbu.rotY = Rotation;
        }
    }
}
