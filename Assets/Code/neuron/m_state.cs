using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_state : module
    {
        neuron main;
        neuron second;

        public action state { get; private set; }
        public int priority {get; private set;} = -1;
        public bool acceptSecondState {get; private set;} = false;

        public action secondState { get; private set; }
        public int secondPriority {get; private set;} = -1;

        List <reflection> reflections = new List<reflection> ();

        public override void Create()
        {
            main = character.ConnectNode (new neuron ());
            second = character.ConnectNode (new neuron ());
        }

        // priority makes state can be overriden by other state with higher priority
        //
        public bool SetState ( action state, int priority, bool DoesAcceptSecondState = false )
        {
            if (priority <= this.priority) return false;

            if (this.state != null)
            EndMainState ();

            main.Set (state);
            this.state = state;
            this.priority = priority;
            acceptSecondState = DoesAcceptSecondState;

            if (!acceptSecondState && secondState != null)
                EndSecondState ();
            
            main.OnNeuronEnd = EndMainState;
            main.Aquire (this);
            return true;
        }

        public bool SetSecondState ( action secondState, int priority)
        {
            if (!acceptSecondState) return false;
            if (priority <= this.secondPriority) return false;
            
            if (this.secondState != null)
                EndSecondState ();

            second.Set (secondState);
            this.secondState = secondState;
            secondPriority = priority;

            second.OnNeuronEnd = EndSecondState;
            second.Aquire (this);
            return true;
        }

        void EndMainState ()
        {
            main.Free (this);
            state = null;
            priority = -1;
            acceptSecondState = false;
        }

        void EndSecondState ()
        {
            second.Free (this);
            secondState = null;
            secondPriority = -1;
        }

        public void AddReflection ( reflection r )
        {
            if (reflections.Contains (r))
                return;
            r.Aquire (this);
            reflections.Add (r);
        }
        
        public void RemoveReflection ( reflection r )
        {
            if (!reflections.Contains (r))
                return;
            r.Free (this);
            reflections.Remove (r);
        }
    }

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

        protected override void OnAquire()
        {
            enabled = true;
            root.iStart ();
        }

        public void Set ( action root )
        {
            if (on)
            throw new InvalidOperationException ("FATAL ERROR: don't set root when neuron is on, will lead to crash");

            this.root = root;
        }

        protected override void OnFree()
        {
            if (root.on)
                root.iAbort();
        }
    }
}
