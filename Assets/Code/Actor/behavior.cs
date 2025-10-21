using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class behavior : controller {
        [link]
        script script;
        
        term current;

        public void set_behavior ( term behavior ) {
            if ( script.contains (current) && script [current].on )
            phoenix.core.stop_action ( script [current] );

            current = behavior;
            phoenix.core.start_action ( script [behavior] );
        }
    }
}