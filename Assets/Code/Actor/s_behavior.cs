using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inkedAttribute]
    public class s_behavior : will
    {
        Lyra.act _script;
        bool FirstFrame = true;

        public class package : ink <s_behavior>
        {
            public package ( ActPaper script )
            {
                o._script = script.GetAction ();
            }
        }

        protected override void harmony()
        {
            sky.add ( _script );
            phoenix.core.start (this);
        }

        protected override void alive()
        {
            if (FirstFrame)
            {
                phoenix.core.start ( _script );
                FirstFrame = false;
            }
        }
    }
}
