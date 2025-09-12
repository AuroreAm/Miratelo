using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // retrieve delta from curve
    public class skin_dir : controller
    {
        public Vector3 dir;
        public float delta {private set; get;}
        float t;

        Animator ani;

        protected override void _ready()
        {
            ani = system.get <skin> ().ani;
        }

        protected override void _start()
        {
            t = 0;
            delta = 0;
        }

        protected override void _step()
        {
            delta = ani.GetFloat(hash.spd) - t;
            if ( delta < 0 ) delta = 0;

            t = ani.GetFloat(hash.spd);
        }

        protected override void _stop()
        {
            delta = 0;
        }
    }
}