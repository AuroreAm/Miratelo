using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public struct curve_delta {
        public AnimationCurve curve { get; private set; }

        public bool on { get; private set; }
        public float current { get; private set; }

        float t;
        float target;
        float duration;

        public curve_delta(AnimationCurve curve) {
            this.curve = curve;
            t = 0;
            target = 0;
            duration = 0;
            on = false;
            current = 0;
        }

        public void start(float target_value, float duration) {
            on = true;

            target = target_value;
            this.duration = duration;

            current = 0;
            t = 0;
        }

        public bool done => t == duration;

        public float tick_delta() {
            if (!on) return 0;

            float a = current;
            t += Time.deltaTime;

            if (t >= duration) {
                t = duration;
                on = false;
            }

            current = curve.Evaluate(t / duration) * target;

            return current - a;
        }

        public void stop ()
        {
            current = target;
            t = duration;
            on = false;
        }

        public void jump_to ( float _t ) {
            if ( _t > t ) {
                t = _t;
                current = curve.Evaluate(t / duration) * target;
            }
        }
    }
}