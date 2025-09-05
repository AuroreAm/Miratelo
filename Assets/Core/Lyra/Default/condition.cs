using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Lyra.Spirit
{
    public abstract class condition : moon
    {
        public abstract bool _check ();

        readonly term zero = new term ("zero");
        public virtual term solution => zero;
    }
}