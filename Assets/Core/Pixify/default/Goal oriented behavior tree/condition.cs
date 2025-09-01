using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Lyra.Spirit
{
    public abstract class condition : pix
    {
        public abstract bool Check ();

        readonly term zero = new term ("zero");
        public virtual term solution => zero;
    }
}