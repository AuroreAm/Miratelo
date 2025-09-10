using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Lyra
{
    public abstract class condition : moon
    {
        public abstract bool _check ();
    }
}