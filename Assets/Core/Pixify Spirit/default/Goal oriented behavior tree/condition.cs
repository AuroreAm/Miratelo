using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Pixify.Spirit
{
    public abstract class condition : pix
    {
        public abstract bool Check ();

        readonly term zero = new term ("zero");
        public virtual term solution => zero;
    }
}