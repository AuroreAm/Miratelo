using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    [inked]
    public class behavior : controller, ruby <system_ready> {
        [link]
        script script;

        term start;
        action start_script;

        public class ink : ink<behavior> {
            public ink(term start) {
                o.start = start;
            }
        }

        protected override void _start() {
            phoenix.core.start_action( start_script );
        }

        public void _radiate(system_ready gleam) {
            start_script =  script[start];
            phoenix.core.execute(this);
        }
    }
}