using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public abstract class reflexion : action
    {
        public void iReflex ()
        {
            if (!on)
            Reflex ();
        }

        /// <summary>
        ///  called by mind when the implementer is not on
        /// </summary>
        protected virtual void Reflex ()
        {}
    }

    [PixiBase]
    public class reaction : pixi.managed
    {}
}