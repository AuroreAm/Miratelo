using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{

    public abstract class core : module, IIntegral
    {
        public integral integral {private set; get;}
        public bool on => integral.on;

        public core ()
        {
            integral = new integral (this, OnAquire, OnFree);
        }

        public abstract void Main ();
        protected virtual void OnAquire() { }
        protected virtual void OnFree() { }
        public void Aquire (atom host) => integral.Aquire (host);
        public void Free (atom host) => integral.Free (host);
    }
}