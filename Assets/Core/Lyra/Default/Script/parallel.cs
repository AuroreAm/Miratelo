using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public abstract class parallel : action_decorator {
        private parallel () {}

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

        public sealed class all : parallel {
            /// true when parallel is starting all child node, used to prevent stopping as all child are not all started
            bool start_frame;
            bool check_stop_first;
            protected override void _start() {
                start_frame = true;
                base._start();
                start_frame = false;
                check_stop ();
            }
            
            void check_stop () {
                if (check_stop_first) {
                    foreach (var n in o)
                        if (n.on)
                            return;

                    stop();
                }
            }
            
            public override void _star_stop(star p) {
                if (!on) return;

                if (start_frame) {
                    check_stop_first = true;
                    return;
                }

                foreach (var n in o)
                    if (n.on)
                        return;

                stop();
            }
        }

        public sealed class first_linked : parallel {
            public override void _star_stop(star p) {
                if (!on) return;

                if (p == o[0])
                    stop();
            }

        }

        public sealed class race : parallel {
            public override void _star_stop(star p) {
                if (!on) return;

                stop();
            }
        }
    }
}