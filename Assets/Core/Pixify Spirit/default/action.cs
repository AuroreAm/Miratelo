using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    [PixiBase]
    public abstract class action : pixi.self_managed
    {
    }

    [Category("debug")]
    public class Log : action
    {
        [Export]
        public string log;

        protected override void Step()
        {
            Debug.Log (log);
            SelfStop ();
        }
    }
}
