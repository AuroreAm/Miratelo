using System;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public sealed class neuron : core
    {   
        public Action OnNeuronEnd { private get; set; }
        action root;

        public override void Main()
        {
            if (root.on)
                root.iExecute();
            if (!root.on)
            {
                OnNeuronEnd?.Invoke ();
                enabled = false;
            }
        }

        public void Aquire ( node host, action root )
        {
            this.root = root;
            Aquire (host);
        }

        protected override void OnAquire()
        {
            enabled = true;
            root.iStart ();
        }

        protected override void OnFree()
        {
            if (root.on)
                root.iAbort();
        }
    }
}
