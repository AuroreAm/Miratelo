using Lyra;
using UnityEngine;
using Triheroes.Code.CapsuleAct;

namespace Triheroes.Code
{
    [path("ground reaction")]
    public class react_fall : action, acting
    {
        [link]
        ground ground;

        [link]
        capsule.gravity gravity;

        [link]
        fall fall;

        [link]
        motor motor;

        public void _act_end(act m)
        { }

        protected override void _step()
        {
            if (!ground && gravity < 0)
                motor.start_act(fall, this);
        }
    }

    [path("ground reaction")]
    public class react_fall_hard : action, acting
    {
        [link]
        ground ground;

        [link]
        capsule.gravity gravity;

        [link]
        fall fall;

        [link]
        fall_hard fallHard;

        [link]
        motor motor;

        float time;

        protected override void _step()
        {
            if ( !ground && gravity < 0 && !(motor.act is fall) )
            {
                if (motor.start_act(fall, this))
                    time = 0;
            }
            else if (motor.act == fall)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    motor.start_act(fallHard, this);
                    time = 0;
                }
            }
        }

        public void _act_end(act m)
        { }
    }
}