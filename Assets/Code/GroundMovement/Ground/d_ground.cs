using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_ground : pix
    {
        /// <summary>
        /// target rotation by ground movements
        /// </summary>
        public float rotY;

        [Depend]
        s_skin ss;

        public static Vector3 SlopeProjection ( Vector3 Dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (Dir, GroundNormal).normalized * Dir.magnitude;

        pixi user;
        public void use ( pixi _user )
        {
            user = _user;
        }
        public bool active => user != null && user.on;

        public void PerformRotation ()
        {
            ss.rotY = Mathf.MoveTowardsAngle(ss.rotY, rotY, Time.deltaTime * 720);
        }

        public void PerformRotation ( float rotY )
        {
            this.rotY = rotY;
            ss.rotY = Mathf.MoveTowardsAngle(ss.rotY, rotY, Time.deltaTime * 720);
        }
    }
}