using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class hit_pause : action
    {
        const float duration = .1f;
        float t;
        
        public void spam ()
        {
            if (on)
            t = 0;
            else
            phoenix.core.start (this);
        }
        
        protected override void _start()
        {
            t = 0;
            Time.timeScale = .1f;
        }

        protected override void _step()
        {
            if (t > duration)
            stop ();

            t += Time.unscaledDeltaTime;
        }

        protected override void _stop()
        {
            Time.timeScale = 1;
        }
    }
}