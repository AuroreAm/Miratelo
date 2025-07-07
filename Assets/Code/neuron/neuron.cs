using System;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public sealed class neuron : piece
    {
        public SuperKey Descriptor;
        public Action OnNeuronEnd { private get; set; }
        public action root { private get; set; }

        public override void Main()
        {
            if (root.on)
                root.iExecute();
            if (!root.on)
            {
                OnNeuronEnd?.Invoke ();
                integral.enabled = false;
            }
        }

        public void Aquire ( atom host, action root )
        {
            this.root = root;
            integral.Aquire (host);
        }

        protected override void OnStart()
        {
            integral.enabled = true;
            root.iStart ();
        }

        protected override void OnFree()
        {
            if (root.on)
                root.iAbort();
        }
    }
}
