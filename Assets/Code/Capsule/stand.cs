using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class stand : moon
    {
        /// <summary>
        /// target rotation by ground movements
        /// </summary>
        public float roty;
        public float anchor { get; private set; }
        float speed = 920;

        [link]
        skin skin;

        public static Vector3 slope_projection ( Vector3 Dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (Dir, GroundNormal).normalized * Dir.magnitude;

        protected override void _ready()
        {
            roty = skin.roty;
        }

        star user;
        public void use ( star _user )
        {
            user = _user;
        }
        public bool active => user != null && user.on;

        public void rotate_skin ()
        {
            skin.roty = Mathf.MoveTowardsAngle(skin.roty, roty, Time.deltaTime * speed);
            
            if ( roty == skin.roty )
            anchor = roty;
        }
    }
}