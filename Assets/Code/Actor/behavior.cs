using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [inked]
    public class behavior : controller {
        [link]
        script script;

        term start;
        int frame;
        bool started;

        public class ink : ink<behavior> {
            public ink(term start) {
                o.start = start;
            }
        }

        protected override void _ready() {
            phoenix.core.execute(this);
            frame = Time.frameCount;
        }

        protected override void _step() {
            if (!started && frame != Time.frameCount) {
                phoenix.core.start_action(script[start]);
                started = true;
            }
        }
    }
}