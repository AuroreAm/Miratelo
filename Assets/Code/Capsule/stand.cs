using System;
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