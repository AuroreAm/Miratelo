using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [NeedPackage]
    public class s_behavior : controller
    {
        action _script;
        bool FirstFrame = true;

        public class package : Package <s_behavior>
        {
            public package ( ActionPaper script )
            {
                o._script = script.GetAction ();
            }
        }

        protected override void OnStructured()
        {
            Structure.Add ( _script );
            SceneMaster.Processor.Start (this);
        }

        protected override void OnStep()
        {
            if (FirstFrame)
            {
                SceneMaster.Processor.Start ( _script );
                FirstFrame = false;
            }
        }
    }
}
