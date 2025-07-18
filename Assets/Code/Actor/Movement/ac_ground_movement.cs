using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public static class ground_movement 
    {
        public static Vector3 SlopeProjection ( Vector3 Dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (Dir, GroundNormal).normalized * Dir.magnitude;
    }
}