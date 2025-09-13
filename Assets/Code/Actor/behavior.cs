using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class behavior : controller
    {
        [link]
        script script;

        term start;
        bool first_frame = true;

        public class ink : ink <behavior>
        {
            public ink ( term start )
            {
                o.start = start;
            }
        }

        protected override void _ready()
        {
            system.add ( script );
            phoenix.core.automatic (this);
        }

        protected override void _step()
        {
            if (first_frame)
            {
                phoenix.core.start_action ( script [start] );
                first_frame = false;
            }
        }
    }
}
