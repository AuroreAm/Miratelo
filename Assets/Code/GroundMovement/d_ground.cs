using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_ground : shard
    {
        /// <summary>
        /// target rotation by ground movements
        /// </summary>
        public float RotY;

        [harmony]
        s_skin skin;

        public static Vector3 SlopeProjection ( Vector3 Dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (Dir, GroundNormal).normalized * Dir.magnitude;

        protected override void harmony()
        {
            RotY = skin.RotY;
        }

        aria user;
        public void Use ( aria _user )
        {
            user = _user;
        }
        public bool active => user != null && user.on;

        public void PerformRotation ()
        {
            skin.RotY = Mathf.MoveTowardsAngle(skin.RotY, RotY, Time.deltaTime * 720);
        }

        public void PerformRotation ( float rotY )
        {
            RotY = rotY;
            skin.RotY = Mathf.MoveTowardsAngle(skin.RotY, rotY, Time.deltaTime * 720);
        }
    }
}