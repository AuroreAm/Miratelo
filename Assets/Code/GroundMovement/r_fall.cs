using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [verse("ground reaction")]
    public class r_fall : act, ILucid
    {
        [harmony]
        d_ground_data ground_data;

        [harmony]
        s_gravity_ccc gravitySys;

        [harmony]
        ac_fall fall;

        [harmony]
        kinesis motor;

        public void inhalt(motor m)
        { }

        protected override void alive()
        {
            if (!ground_data.onGround && gravitySys.Gravity < 0)
                motor.perform(fall, this);
        }

    }


    [verse("ground reaction")]
    public class r_fall_with_hard : act, ILucid
    {
        [harmony]
        d_ground_data groundData;

        [harmony]
        s_gravity_ccc gravitySys;

        [harmony]
        ac_fall fall;

        [harmony]
        ac_fall_hard fallHard;

        [harmony]
        kinesis motor;

        float time;

        protected override void alive()
        {
            if ( !groundData.onGround && gravitySys.Gravity < 0 && !(motor.state is ac_fall) )
            {
                if (motor.perform(fall, this))
                    time = 0;
            }
            else if (motor.state == fall)
            {
                time += Time.deltaTime;
                if (time > 0.5f)
                {
                    motor.perform(fallHard, this);
                    time = 0;
                }
            }
        }

        public void inhalt(motor m)
        { }
    }

}
