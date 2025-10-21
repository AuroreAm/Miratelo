using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [need_ready]
    public class buffer : action {
        
        #if UNITY_EDITOR
        public float get_t => t;
        #endif
        
        float t;
        float duration;

        public buffer ( float _duration ) {
            duration = _duration;
        }

        public void stack () {
            if ( !on ) {
                ready_for_tick ();
                phoenix.core.start_action (this);
            }
            t = duration;
        }

        protected override void _step() {
            if ( t <= 0 ) {
                stop ();
            }

            t -= Time.deltaTime;
        }

        public void clear () {
            stop ();
        }

    }
}
