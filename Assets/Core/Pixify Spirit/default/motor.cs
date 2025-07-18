using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Pixify.Spirit
{
    public abstract class motor : pixi.self_managed
    {
        public abstract int Priority {get;}
        public virtual bool AcceptSecondState {get;} = false;
    }

    public interface IMotorHandler
    {
        public void OnMotorEnd (motor m);
    }
}