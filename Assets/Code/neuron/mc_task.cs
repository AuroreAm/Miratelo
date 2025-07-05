using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // task layer of behavior
    // state that manipulate control motor states
    public class mc_task : module
    {
        neuron TaskExecutor;
        action Task;

        public override void Create()
        {
            TaskExecutor = character.ConnectNode ( new neuron () );
        }

        public void SetTask ( action task )
        {
            if (Task != null)
            EndTask ();

            Task = task;
            TaskExecutor.OnNeuronEnd = EndTask;
            TaskExecutor.Aquire ( this, task );
        }

        void EndTask ()
        {
            TaskExecutor.Free (this);
            Task = null;
        }
    }
}