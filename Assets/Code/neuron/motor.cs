using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class motor : action
    {
        public abstract int Priority {get;}
        public virtual bool AcceptSecondState {get;} = false;
    }
}