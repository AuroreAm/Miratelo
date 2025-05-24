using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class core : module
    {
        bool _on;
        protected bool enabled;
        public bool on { get { return enabled && _on; } }
        public bool aquired => on;

        public sealed override void Create()
        {
            PixifyEngine.o.Register (this);
            Create1 ();
        }

        public virtual void Create1 () {}
        public abstract void Main ();

        node host;
        public void Aquire(node host)
        {
            if (!_on)
            {
                _on = true;
                enabled = true;
                this.host = host;
                OnAquire();
            }
            else if (aquired)
                throw new InvalidOperationException(GetType().Name + " cannot be aquired, is already aquired");
        }

        protected virtual void OnAquire() { }
        protected virtual void OnFree() { }

        public void Free(node host)
        {
            if (_on && this.host == host)
            {
                _on = false;
                enabled = false;
                OnFree();
                this.host = null;
            }
            else
                throw new InvalidOperationException("cannot free things this node doesn't host, or the node is no longuer aquired in the first place");
        }
    }

    public interface ICoreFeedback
    {
        public void AquiredNodeFreed(node AquiredNode);
    }
}