using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class a_hook_attack : virtus.pixi
    {
        [Depend]
        a_slash_attack asa;

        AnimationCurve curve;
        Vector3 dir;
        float duration;

        static AnimationCurve _hookCurve;
        static Vector3 _hookDir;
        static float _duration;
        public static void Fire ( int name, Sword sword, float duration, AnimationCurve hookCurve, Vector3 hookDir )
        {
            _hookCurve = hookCurve;
            _hookDir = hookDir;
            _duration = duration;
            // a_slash_attack.Fire ( name, sword, duration );
        }

        protected override void Start()
        {
            curve = _hookCurve;
            dir = _hookDir;
            duration = _duration;
        }

        protected override void Create1()
        {
            // asa.Link (Hit);
        }

        void Hit (int hitted)
        {
            if (Element.Contains (hitted))
            Element.SendMessage ( hitted, new Hook ( dir, curve, duration ) );
        }
    }

    public struct Hook
    {
        public Vector3 dir;
        public float duration;
        public AnimationCurve curve;

        public Hook ( Vector3 Dir, AnimationCurve Curve, float Duration )
        {
            curve = Curve;
            dir = Dir;
            duration = Duration;
        }
    }
}
