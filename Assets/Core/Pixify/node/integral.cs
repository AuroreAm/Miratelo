using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public interface IIntegral
    {
        public void Main ();
    }
    
    [AttributeUsage(AttributeTargets.Class,Inherited = false)]
    public class IntegralBaseAttribute : Attribute
    {}

    public class integral : atom
    {
        public IIntegral host {private set; get;}
        Action OnAquire;
        Action OnFree;

        bool _on;
        public bool enabled;
        public bool on { get { return enabled && _on; } }
        public bool aquired => on;

        public integral (IIntegral host, Action OnAquire, Action OnFree)
        {
            this.host = host;
            this.OnAquire = OnAquire;
            this.OnFree = OnFree;
            PixifyEngine.o.Register (this);
        }

        public void Main ()
        {
            host.Main ();
        }

        atom owner;
        public void Aquire(atom owner)
        {
            if (!_on)
            {
                _on = true;
                enabled = true;
                this.owner = owner;
                OnAquire();
            }
            else if (aquired)
                throw new InvalidOperationException(this.host.GetType().Name + " cannot be aquired, is already aquired");
        }

        public void Free(atom owner)
        {
            if (_on && this.owner == owner)
            {
                _on = false;
                enabled = false;
                OnFree();
                this.owner = null;
            }
            else
                throw new InvalidOperationException("cannot free things this atom doesn't own, or the atom is no longuer aquired in the first place");
        }
    }
}
