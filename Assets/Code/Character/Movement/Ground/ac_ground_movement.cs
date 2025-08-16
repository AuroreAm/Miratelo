using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_ground : pix
    {
        /// <summary>
        /// target rotation by ground movements
        /// </summary>
        public Vector3 rotY;
        public static Vector3 SlopeProjection ( Vector3 Dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (Dir, GroundNormal).normalized * Dir.magnitude;
    }
}