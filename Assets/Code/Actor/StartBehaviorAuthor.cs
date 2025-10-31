using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code {

    public class StartBehaviorAuthor : ActorAuthorModule {

        public string [] Starts;

        public override void _create() {
            term [] starts = new term [Starts.Length];
            for (int i = 0; i < starts.Length; i++) {
                starts  [i] = new term ( Starts [i] );
            }
            new start_behavior ( starts );
        }
    }

    public class start_behavior : moon, ruby<system_written> {
        term [] start;

        [link]
        behavior behavior;

        public start_behavior ( term [] _start ) {
            start = _start;
        }

        public void _radiate(system_written gleam) {
            for (int i = 0; i < start.Length; i++) {
                behavior.start ( start [i] );
            }
        }
    }
}