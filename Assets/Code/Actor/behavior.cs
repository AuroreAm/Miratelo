using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class behavior : controller
    {
        action script;
        bool first_frame = true;

        public class ink : ink <behavior>
        {
            public ink ( ActionPaper script )
            {
                o.script = script.write ();
            }
        }

        protected override void _ready()
        {
            system.add ( script );
            phoenix.core.start (this);
        }

        protected override void _step()
        {
            if (first_frame)
            {
                phoenix.core.start ( script );
                first_frame = false;
            }
        }
    }
}
