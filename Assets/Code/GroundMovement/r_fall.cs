using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [Path("ground reaction")]
    public class r_fall : action, IMotorHandler
    {
        [Link]
        d_ground_data ground_data;

        [Link]
        s_gravity_ccc gravitySys;

        [Link]
        ac_fall fall;

        [Link]
        s_motor motor;

        public void OnMotorEnd(motor m)
        { }

        protected override void OnStep()
        {
            if (!ground_data.onGround && gravitySys.Gravity < 0)
                motor.SetState(fall, this);
        }

    }


    [Path("ground reaction")]
    public class r_fall_with_hard : action, IMotorHandler
    {
        [Link]
        d_ground_data groundData;

        [Link]
        s_gravity_ccc gravitySys;

        [Link]
        ac_fall fall;

        [Link]
        ac_fall_hard fallHard;

        [Link]
        s_motor motor;

        float time;

        protected override void OnStep()
        {
            if ( !groundData.onGround && gravitySys.Gravity < 0 && !(motor.State is ac_fall) )
            {
                if (motor.SetState(fall, this))
                    time = 0;
            }
            else if (motor.State == fall)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    motor.SetState(fallHard, this);
                    time = 0;
                }
            }
        }

        public void OnMotorEnd(motor m)
        { }
    }

}
