using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class behavior : controller, ruby <actor_created>
    {
        [link]
        script script;

        term start;

        public class ink : ink <behavior>
        {
            public ink ( term start )
            {
                o.start = start;
            }
        }
        
        public void _radiate(actor_created gleam)
        {
            phoenix.core.automatic (this);
        }

        protected override void _ready()
        {
            system.add ( script );
        }

        protected override void _start()
        {
            phoenix.core.start_action ( script [start] );
        }
    }
}
