using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code.Axeal
{
    public struct force_curve_data {
        public Vector3 dir;
        public float duration;
        public AnimationCurve curve;
        public int flag;

        public force_curve_data ( Vector3 _dir, float _duration, AnimationCurve _curve, int _flag = 0 ) {
            dir = _dir;
            duration = _duration;
            curve = _curve;
            flag = _flag;
        }

        public force_curve_data (  float _duration, AnimationCurve _curve, int _flag = 0 ) {
            dir = Vector3.zero;
            duration = _duration;
            curve = _curve;
            flag = _flag;
        }
    }

    public struct force_curve {
        curve_delta cu;
        Vector3 dir;

        //1 : no gravity
        public int flag;
        public bool done => cu.done;
        public float current => cu.current;

        public force_curve ( force_curve_data _data ) {
            cu = new curve_delta ( _data.curve );
            dir = _data.dir.normalized;
            cu.start ( _data.dir.magnitude, _data.duration );
            flag = _data.flag;
        }

        public void deviate ( Vector3 _dir ) => dir = _dir.normalized;

        public void jump_to ( float t ) {
            cu.jump_to (t);
        }

        public Vector3 tick () {
            return cu.tick_delta () * dir;
        }

        public void stop () {
            cu.stop ();
        }
    }
}