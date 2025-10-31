using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    public class behavior : moon {
        [link]
        script script;
        
        public void start ( term name ) {
            phoenix.core.start_action ( script [name] );
        }
    }
}