using System.Collections.Generic;
using Lyra;

namespace Triheroes.Code {
    public class behavior : moon {
        [link]
        script script;

        Dictionary < term, action > library = new Dictionary<term, action> ();

        public void start_branch ( term branch, term script_index ) {
            if ( library.ContainsKey (branch) ) {
                if ( library [branch].on ) {
                    phoenix.core.stop_action ( library [branch] );
                }
                library [branch] = script [script_index];
            }
            else library.Add ( branch, script [script_index] );

            phoenix.core.start_action ( script [script_index] );
        }

        public void remove_branch ( term branch ) {
            phoenix.core.stop_action ( library [branch] );
        }
    }
}