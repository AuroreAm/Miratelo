using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public class r_idle : reflexion, IMotorHandler
    {
        [Depend]
        s_motor sm;

        [Depend]
        ac_idle ai;

        public void OnMotorEnd(motor m)
        {
            SelfStop ();
        }

        protected override void Reflex()
        {
            if (sm.state == null)
                if (sm.SetState (ai, this))
                    Stage.Start (this);
        }
    }

    public class ac_idle : motor
    {
        public override int Priority => Pri.def;

        [Depend]
        s_capsule_character_controller sccc;
        [Depend]
        s_gravity_ccc sgc;
        [Depend]
        d_ground dg;
        [Depend]
        s_skin ss;

        public override void Create()
        {
            Link (sccc);
            Link (sgc);
        }

        protected override void Start()
        {
            dg.use (this);
            ss.PlayState (0, AnimationKey.idle,0.1f);
        }

        protected override void Step()
        {
            dg.PerformRotation ();
        }
    }

    public class ac_look : action
    {
        public float y;
        
        [Depend]
        d_ground dg;
        [Depend]
        s_skin ss;

        protected override void Start()
        {
            dg.rotY = y;
        }

        protected override void Step()
        {
            if ( ss.rotY == dg.rotY || !dg.active )
            SelfStop ();
        }
    }
}
