using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class delta_curve : atom
    {
        AnimationCurve curve;

        public bool on {get; private set;}
        public float currentValue {get; private set;}
        float t;

        float TargetX;
        float duration;

        public void Start ( float endValue, float duration )
        {
            on = true;

            TargetX = endValue;
            this.duration = duration;

            currentValue = 0;
            t = 0;
        }

        public bool Done => t == duration;

        public float TickDelta ()
        {
            if (!on) return 0;

            float a = currentValue;
            t += Time.deltaTime;

            if (t>=duration)
            {
            t = duration;
            on = false;
            }

            currentValue = curve.Evaluate ( t/duration ) * TargetX;

            return currentValue - a;
        }

        public delta_curve ( AnimationCurve curve )
        {
            this.curve = curve;
        }
    }
}
