using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class hooker : attack, gold <hacked>
    {
        AnimationCurve cu;
        Vector3 dir;
        float duration;

        
        #region fire
        static AnimationCurve _hook_curve;
        static Vector3 _hook_dir;
        static float _duration;

        public class w : slash.w {
            public void fire ( sword sword, slay.path path, float duration, AnimationCurve hook_curve, Vector3 hook_dir, float hook_duration ) {
                _hook_curve = hook_curve;
                _hook_dir = hook_dir;
                _duration = hook_duration;
                fire ( sword, path, duration );
            }
        }
        #endregion

        protected override void _start()
        {
            cu = _hook_curve;
            dir = _hook_dir;
            duration = _duration;
        }

        public void _radiate(hacked gleam)
        {
            pallas.radiate ( gleam.hacked_id, new hook ( dir, cu, duration ) );
        }
    }

    public struct hook
    {
        public Vector3 dir;
        public float duration;
        public AnimationCurve curve;

        public hook ( Vector3 _dir, AnimationCurve _curve, float _duration )
        {
            curve = _curve;
            dir = _dir;
            duration = _duration;
        }
    }

    public class hook_spammer : attack, gold<hacked>
    {
        public void _radiate(hacked gleam)
        {
            pallas.radiate ( gleam.hacked_id, new hook_spam () );
        }
    }

    public struct hook_spam
    {}
}
