using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class task : action
    {
        public virtual bool doable ()
        {
            return true;
        }
    }
}
