using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public abstract class parallel : decorator {
        protected override void _start() {
            foreach (star p in o)
                p.tick(this);
        }

        protected override void _step() {
            foreach (star p in o)
                if (p.on)
                    p.tick(this);
        }

        protected override void _stop() {
            foreach (var p in o)
                if (p.on)
                    p.abort(this);
        }

        public class all : parallel {
            public override void _star_stop(star p) {
                if (!on) return;

                foreach (var n in o)
                    if (n.on)
                        return;

                stop();
            }
        }

        public class first_linked : parallel {
            public override void _star_stop(star p) {
                if (!on) return;

                if (p == o[0])
                    stop();
            }

        }

        public class race : parallel {
            public override void _star_stop(star p) {
                if (!on) return;

                stop();
            }
        }
    }
}