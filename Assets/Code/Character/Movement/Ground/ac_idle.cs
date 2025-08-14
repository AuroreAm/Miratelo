using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    // will set as idle if the character has nothing to do
    public class r_idle : reflexion, IMotorHandler
    {
        [Depend]
        s_motor sm;
        [Depend]
        ac_idle ai;

        public void OnMotorEnd(motor m)
        {}

        protected override void Step()
        {
            if (sm.state == null)
                sm.SetState (ai, this);
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
        s_skin ss;

        public override void Create()
        {
            Link (sccc);
            Link (sgc);
        }

        protected override void Start()
        {
            ss.PlayState (0, AnimationKey.idle,0.1f);
        }
    }
}
