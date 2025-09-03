using UnityEngine;

namespace Lyra
{
    [SysBase(-1)]
    public class action : sys.self
    {
    }

    [Path("debug")]
    public class log : action
    {
        [Export]
        public string Text;

        protected override void OnStep()
        {
            Debug.Log (Text);
            Stop ();
        }
    }
}