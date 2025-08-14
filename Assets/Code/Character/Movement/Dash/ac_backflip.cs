using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class ac_backflip : motor
    {
        public override int Priority => Pri.Action;

        [Depend]
        s_capsule_character_controller sccc; int key_ccc;
        [Depend]
        s_skin ss;

        delta_curve movement;
        delta_curve jump;
        float JumpHeight = 1.5f;

        public override void Create()
        {
            movement = new delta_curve(SubResources<CurveRes>.q(new term("backflip")).Curve);
            jump = new delta_curve(SubResources<CurveRes>.q(new term("jump")).Curve);
        }

        protected override void Start()
        {
            key_ccc = Stage.Start(sccc);
            ss.PlayState(0, AnimationKey.backflip, 0.1f, BackFlipEnd);

            movement.Start(5, ss.DurationOfState(AnimationKey.backflip));
            jump.Start ( JumpHeight, .5f );
        }

        protected override void Step()
        {
            sccc.dir += Vecteur.LDir(ss.rotY,Vector3.back) * movement.TickDelta () + new Vector3(0, jump.TickDelta() , 0);
        }

        void BackFlipEnd()
        {
            SelfStop();
        }

        protected override void Stop()
        {
            Stage.Stop(key_ccc);
        }
    }
}
