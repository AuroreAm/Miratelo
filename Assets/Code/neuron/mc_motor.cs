using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // motor layer of behavior
    // asign only state that directly manipulate motion of a character: animation and movement
    public class mc_motor : module
    {
        neuron main;
        neuron second;

        public motor state { get; private set; }
        public int priority {get; private set;} = -1;
        public bool acceptSecondState {get; private set;} = false;

        public motor secondState { get; private set; }
        public int secondPriority {get; private set;} = -1;

        List <reflection> reflections = new List<reflection> ();

        public override void Create()
        {
            main = character.ConnectNode (new neuron ());
            second = character.ConnectNode (new neuron ());
        }

        // priority makes state can be overriden by other state with higher priority
        public bool SetState ( motor state )
        {
            if (state.Priority <= this.priority) return false;

            if (this.state != null)
            EndMainState ();

            this.state = state;
            this.priority = state.Priority;
            acceptSecondState = state.AcceptSecondState;

            if (!acceptSecondState && secondState != null)
                EndSecondState ();
            
            main.OnNeuronEnd = EndMainState;
            main.Aquire (this, state);
            return true;
        }

        public bool SetSecondState ( motor secondState)
        {
            if (!acceptSecondState) return false;
            if (state.Priority <= this.secondPriority) return false;
            
            if (this.secondState != null)
                EndSecondState ();

            this.secondState = secondState;
            secondPriority = state.Priority;

            second.OnNeuronEnd = EndSecondState;
            second.Aquire (this, secondState);
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

        public void ClearReflection ()
        {
            foreach ( reflection r in reflections )
            r.Free (this);
            reflections.Clear ();
        }
    }

}
