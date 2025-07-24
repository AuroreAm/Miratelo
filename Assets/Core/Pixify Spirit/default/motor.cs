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
        /// <summary>
        /// called when the motor is stopped, only called when the handler is on
        /// </summary>
        /// <param name="m"></param>
        public void OnMotorEnd(motor m);
        public bool on { get; }
    }
}