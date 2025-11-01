using System;
using Lyra;

namespace Triheroes.Code
{
    public class BehaviorExtAuthor2 : ExtWriter {
        public BehaviorPair [] Start;
        protected override void WriteTo() {
            var behavior = s.get <behavior> ();

            foreach ( var p in Start )
            behavior.start_branch ( new term ( p.Branch ), new term (p.ScriptIndex ) );
        }

        
    }

    [Serializable]
    public struct BehaviorPair {
        public string Branch;
        public string ScriptIndex;
    }
}
