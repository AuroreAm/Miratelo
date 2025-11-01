using System;
using Lyra;

namespace Triheroes.Code
{
    public class BehaviorExtAuthor1 : ExtWriter {
        public ActionPaper [] Start;
        protected override void WriteTo() {
            var behavior = s.get <behavior> ();

            foreach ( var p in Start )
            behavior.start_branch ( p.formal_term (), p.formal_term () );
        }
    }
}
