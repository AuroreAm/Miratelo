using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu ( menuName = "RPG/Curve" )]
    public class CurveRes : ScriptableObject
    {
        public AnimationCurve curve;
        public static implicit operator AnimationCurve ( CurveRes a ) => a.curve;
    }
}