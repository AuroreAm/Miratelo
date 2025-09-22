using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using UnityEngine.UI;

namespace Triheroes.Code {

    [path("scene")]
    public class fade_from_black : action
    {
        protected override void _start() {
            ui.o.full_overlay.start ( Color.black, Color.clear );
            stop ();
        }
    }

    [need_ready]
    [inked]
    public class full_overlay : graphic_element {
        public class ink : ink<full_overlay> {
            public ink(RawImage image) {
                o.image = image;
            }
        }
        
        RawImage image;
        Color A, B;
        float t;
        const float duration = 0.5f;

        protected override void _ready() {
            image.gameObject.SetActive (false);
        }

        public void start (Color A, Color B) {
            image.gameObject.SetActive (true);
            this.A = A;
            this.B = B;
            t = 0;

            ready_for_tick ();
            phoenix.core.start_action (this);
        }

        protected override void _step() {
            if ( t >= duration ) {
                stop ();
            }

            t += Time.deltaTime;
            image.color = Color.Lerp (A, B, t / duration);
        }

        protected override void _stop() {
            image.gameObject.SetActive (false);
        }
    }
}