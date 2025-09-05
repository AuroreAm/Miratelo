using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class delta_curve : moon
    {
        public AnimationCurve curve { get; private set; }

        public bool on { get; private set; }
        public float current { get; private set; }

        float t;

        float target;
        float duration;

        public void start ( float target_value, float duration )
        {
            on = true;

            target = target_value;
            this.duration = duration;

            current = 0;
            t = 0;
        }

        public bool done => t == duration;

        public float tick_delta ()
        {
            if (!on) return 0;

            float a = current;
            t += Time.deltaTime;

            if ( t>=duration )
            {
                t = duration;
                on = false;
            }

            current = curve.Evaluate ( t/duration ) * target;

            return current - a;
        }

        public delta_curve ( AnimationCurve curve )
        {
            this.curve = curve;
        }
    }
}