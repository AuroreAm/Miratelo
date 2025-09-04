using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class delta_curve : shard
    {
        public AnimationCurve Curve { get; private set; }

        public bool on { get; private set; }
        public float CurrentValue { get; private set; }

        float t;

        float TargetX;
        float duration;

        public void Start ( float endValue, float duration )
        {
            on = true;

            TargetX = endValue;
            this.duration = duration;

            CurrentValue = 0;
            t = 0;
        }

        public bool Done => t == duration;

        public float TickDelta ()
        {
            if (!on) return 0;

            float a = CurrentValue;
            t += Time.deltaTime;

            if ( t>=duration )
            {
                t = duration;
                on = false;
            }

            CurrentValue = Curve.Evaluate ( t/duration ) * TargetX;

            return CurrentValue - a;
        }

        public delta_curve ( AnimationCurve curve )
        {
            this.Curve = curve;
        }
    }
}