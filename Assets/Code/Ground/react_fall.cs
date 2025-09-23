using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code
{
    [path("ground reaction")]
    public class react_fall : action
    {
        [link]
        ground ground;

        [link]
        gravity gravity;

        [link]
        fall fall;

        [link]
        motor motor;

        protected override void _step()
        {
            if (!ground && gravity < 0)
                motor.start_act(fall);
        }
    }

    [path("ground reaction")]
    public class react_fall_hard : action
    {
        [link]
        ground ground;

        [link]
        gravity gravity;

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
                if (motor.start_act(fall))
                    time = 0;
            }
            else if (motor.act == fall)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    motor.start_act(fallHard);
                    time = 0;
                }
            }
        }
    }
}