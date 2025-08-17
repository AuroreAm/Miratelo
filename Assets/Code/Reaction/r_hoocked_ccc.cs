using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;


namespace Triheroes.Code
{/*
    public class r_hooked_up_ccc : reflexion, IMotorHandler, IElementListener<Hook>
    {
        [Depend]
        s_element se;

        [Depend]
        ac_hooked_up_ccc ahuc;

        [Depend]
        s_motor sm;

        protected override void Start()
        {
            se.LinkMessage (this);
        }

        protected override void Stop()
        {
            se.UnlinkMessage (this);
        }

        public void OnMessage(Hook context)
        {
            ahuc.Set ( context.curve, context.dir, context.duration );

            if ( !ahuc.on )
            sm.SetState ( ahuc, this );
        }

        public void OnMotorEnd(motor m) {}
    }

    public class ac_hooked_up_ccc : motor
    {
        public override int Priority => Pri.ForcedAction;

        [Depend]
        s_capsule_character_controller sccc;

        float t;
        const float TimeOut = 1;

        delta_curve cu;
        Vector3 dir;

        public void Set ( AnimationCurve Curve, Vector3 Dir, float Duration )
        {
            cu = new delta_curve ( Curve );
            dir = Dir;
            cu.Start ( Dir.y, Duration );
            t = 0;
        }

        public override void Create()
        {
            Link (sccc);
        }

        protected override void Start()
        {
            t = 0;
        }

        protected override void Step()
        {
            sccc.dir += dir * cu.TickDelta ();
            t += Time.deltaTime;
            
            if (t > TimeOut)
            SelfStop ();
        }
    }*/
}