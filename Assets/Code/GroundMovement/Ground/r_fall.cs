using System.Collections;
using UnityEngine;
using Lyra;
using Lyra.Spirit;

namespace Triheroes.Code
{
    // will tell the character to fall when on ground
    public class r_fall : reflexion, IMotorHandler
    {
        [Depend]
        d_ground_data dgd;

        [Depend]
        s_gravity_ccc sgc;

        [Depend]
        ac_fall af;

        [Depend]
        s_motor sm;

        public void OnMotorEnd(motor m)
        {}

        protected override void Reflex()
        {
            if (!dgd.onGround && sgc.gravity < 0)
                sm.SetState (af, this);
        }
    }

    public class r_fall_with_hard : reflexion, IMotorHandler
    {
        [Depend]
        d_ground_data dgd;

        [Depend]
        s_gravity_ccc sgc;

        [Depend]
        ac_fall af;

        [Depend]
        ac_fall_hard afh;
        
        [Depend]
        s_motor sm;

        float time;

        protected override void Reflex()
        {
            if (!dgd.onGround && sgc.gravity < 0 && !(sm.state is ac_fall))
            {
                if (sm.SetState (af, this))
                time = 0;
            }
            else if (sm.state == af)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    sm.SetState (afh, this);
                    time = 0;
                }
            }
        }

        public void OnMotorEnd(motor m)
        {}
    }
}