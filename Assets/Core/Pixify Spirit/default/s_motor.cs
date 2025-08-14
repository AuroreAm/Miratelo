using System;
using System.Collections.Generic;
using System.Text;
using Pixify;
using UnityEngine;

namespace Pixify.Spirit
{
    // motor layer of behavior
    // asign only state that directly manipulate motion of a character: animation and movement
    public class s_motor : spirit, IPixiHandler
    {

        public motor state { get; private set; }
        IMotorHandler main;
        public int priority {get; private set;} = -1;
        public bool acceptSecondState {get; private set;} = false;

        public motor secondState { get; private set; }
        IMotorHandler second;
        public int secondPriority {get; private set;} = -1;

        public override void Create()
        {
            Stage.Start (this);
        }

        protected override void Step()
        {
            if (state != null && state.on)
                state.Tick(this);

            if (secondState != null && secondState.on)
                secondState.Tick(this);
        }

        // priority makes state can be overriden by other state with higher priority
        public bool SetState ( motor _state, IMotorHandler handler )
        {
            if (_state.Priority <= priority) return false;

            if (state != null)
            state.ForceStop (this);

            state = _state;
            priority = state.Priority;
            acceptSecondState = state.AcceptSecondState;

            main = handler;

            if (!acceptSecondState && secondState != null)
                secondState.ForceStop (this);

            state.Tick (this);
            return true;
        }

        /// <summary>
        /// end the main state at request of the original handler only
        /// </summary>
        /// <param name="handler"></param>
        public void EndState ( IMotorHandler handler )
        {
            if ( handler == main )
            state.ForceStop (this);
            else
            Debug.LogError (" handler can't stop state ");
        }

        public bool SetSecondState ( motor _secondState, IMotorHandler handler )
        {
            if (!acceptSecondState) return false;
            if (_secondState.Priority <= secondPriority) return false;
            
            if (secondState != null)
                secondState.ForceStop (this);

            second = handler;

            secondState = _secondState;
            secondPriority = _secondState.Priority;

            secondState.Tick (this);
            return true;
        }

        /// <summary>
        /// end the main state at request of the original handler only
        /// </summary>
        /// <param name="handler"></param>
        public void EndSecondState ( IMotorHandler handler )
        {
            if ( handler == second )
            secondState.ForceStop (this);
            else
            Debug.LogError (" handler can't stop state ");
        }

        void EndMainState ()
        {
            var m = state;
            var h = main;

            state = null;
            main = null;
            priority = -1;
            
            acceptSecondState = false;

            if (h.on)
            h.OnMotorEnd (m);
        }

        void EndSecondState ()
        {
            var m =secondState;
            var h = second;

            secondState = null;
            second = null;
            secondPriority = -1;

            if (h.on)
            h.OnMotorEnd (m);
        }

        public void OnPixiEnd(pixi p)
        {
            if ( p == state )
            EndMainState ();

            if ( p == secondState )
            EndSecondState ();
        }
    }

}
