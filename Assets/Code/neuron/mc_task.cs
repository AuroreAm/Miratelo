using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class mc_task : module
    {
        [Depend]
        m_cortex mc;

        [Depend]
        m_blackboard mb;

        List <neuron> ActiveTask = new List<neuron> ();
        List <SuperKey> PendingTask = new List<SuperKey> ();

        public override void Create()
        {
            NeuronPool =  new AtomPool<neuron> (null, NeuronBeforeReturn, NeuronAfterInstance);
        }

        AtomPool <neuron> NeuronPool;

        void NeuronAfterInstance (neuron neuron)
        {
            neuron.OnNeuronEnd = () => OnNeuronEnd (neuron);
        }
        void NeuronBeforeReturn (neuron neuron)
        {
            neuron.Free (this);
            ActiveTask.Remove (neuron);
        }

        public enum TaskAdditionResult { success, failed, pending };
        // NOTE pending means the task is added to the pending task list, and the task that fullfill the condition executes first, but it still can fail // success is the only sure that the task will execute

        public TaskAdditionResult AddTask ( SuperKey TaskID )
        {
            var task = mc.cortex.GetTask (TaskID);
            if (task == null) return TaskAdditionResult.failed;

            if ( IsTaskRunning (TaskID) ) return TaskAdditionResult.failed;

            var preconditions = task.GetPreconditions ();

            foreach (KeyValuePair<int, object> entry in preconditions)
            {
                if (!mb.bb.Compare (entry.Key, entry.Value))
                {
                    var TaskBefore = mc.cortex.GetTaskForEffect (entry.Key, entry.Value);
                    if (TaskBefore != AIKeys.zero)
                    {
                        AddTask (TaskBefore);
                        PendingTask.Add (TaskID);
                        return TaskAdditionResult.pending;
                    }
                    else
                        return TaskAdditionResult.failed;
                }
            }

            var neuron = NeuronPool.GetAtom ();
            neuron.Aquire (this, task);
            neuron.Descriptor = TaskID;
            ActiveTask .Add ( neuron );
            return TaskAdditionResult.success;
        }
        
        Dictionary <neuron, SuperKey> AfterTask = new Dictionary<neuron, SuperKey> ();
        public TaskAdditionResult AddTaskAfter ( SuperKey TaskID, SuperKey TaskBeforeAdd )
        {
            if (!IsTaskRunning (TaskBeforeAdd))
                return TaskAdditionResult.failed;

            var task = mc.cortex.GetTask (TaskID);
            if (task == null) return TaskAdditionResult.failed;
            
            var preconditions = task.GetPreconditions ();

            foreach (KeyValuePair<int, object> entry in preconditions)
            {
                if (!mb.bb.Compare (entry.Key, entry.Value))
                {
                    var TaskBefore = mc.cortex.GetTaskForEffect (entry.Key, entry.Value);
                    if (TaskBefore != AIKeys.zero)
                        break;
                    else
                        return TaskAdditionResult.failed;
                }
            }

            AfterTask.AddOrChange (NeuronOfTask (TaskBeforeAdd), TaskID);
            return TaskAdditionResult.pending;
        }

        public bool IsTaskRunning ( SuperKey TaskID ) => ActiveTask.Exists (x => x.Descriptor == TaskID);

        neuron NeuronOfTask ( SuperKey TaskID ) => ActiveTask.Find (x => x.Descriptor == TaskID);

        void Rethink ()
        {
            for (int i = PendingTask.Count - 1; i >= 0; i--)
            {
                if (AddTask (PendingTask [i]) == TaskAdditionResult.success)
                PendingTask.RemoveAt (i);
            }
        }

        void OnNeuronEnd ( neuron neuron )
        {
            NeuronPool.ReturnAtom (neuron);

            if (AfterTask.ContainsKey (neuron))
            {
                var TaskID = AfterTask [neuron];
                AfterTask.Remove (neuron);
                AddTask (TaskID);
            }

            Rethink ();
        }
    }
}