using System;
using System.Collections.Generic;
using System.Text;
using Lyra;
using UnityEngine;

namespace Lyra
{
    // motor layer of behavior
    // asign only state that directly manipulate motion of a character: animation and movement
    public class s_motor : controller, ISysProcessor
    {
        IMotorHandler _handler;
        
        IMotorHandler _handler2nd;

        public motor State { get; private set; }
        public int Priority {get; private set;} = -1;
        public bool AcceptSecondState {get; private set;} = false;
        public motor State2nd { get; private set; }
        public int Priority2nd {get; private set;} = -1;

        protected override void OnStructured()
        {
            SceneMaster.Processor.Start (this);
        }

        protected override void OnStep()
        {
            if (State != null && State.on)
                State.Tick(this);

            if (State2nd != null && State2nd.on)
                State2nd.Tick(this);
        }

        // priority makes state can be overriden by other state with higher priority
        public bool SetState ( motor state, IMotorHandler handler )
        {
            if (state.Priority <= Priority) return false;

            if (State != null)
            State.ForceStop (this);

            State = state;
            Priority = State.Priority;
            AcceptSecondState = State.AcceptSecondState;

            _handler = handler;

            if (!AcceptSecondState && State2nd != null)
                State2nd.ForceStop (this);

            SceneMaster.Processor.FakeStart (State);
            State.Tick (this);
            return true;
        }

        /// <summary>
        /// end the main state at request of the original handler only
        /// </summary>
        /// <param name="handler"></param>
        public void EndState ( IMotorHandler handler )
        {
            if ( handler == _handler )
            State.ForceStop (this);
            else
            Debug.LogError (" handler can't stop state ");
        }

        public bool SetSecondState ( motor state2nd, IMotorHandler handler )
        {
            if (!AcceptSecondState) return false;
            if (state2nd.Priority <= Priority2nd) return false;
            
            if (State2nd != null)
                State2nd.ForceStop (this);

            _handler2nd = handler;

            State2nd = state2nd;
            Priority2nd = state2nd.Priority;

            SceneMaster.Processor.FakeStart (State2nd);
            State2nd.Tick (this);
            return true;
        }

        /// <summary>
        /// end the main state at request of the original handler only
        /// </summary>
        /// <param name="handler"></param>
        public void EndSecondState ( IMotorHandler handler )
        {
            if ( handler == _handler2nd )
            State2nd.ForceStop (this);
            else
            Debug.LogError (" handler can't stop state ");
        }

        void End ()
        {
            var m = State;
            var h = _handler;

            State = null;
            _handler = null;
            Priority = -1;
            
            AcceptSecondState = false;

            if (h.on)
            h.OnMotorEnd (m);
        }

        void End2nd ()
        {
            var m =State2nd;
            var h = _handler2nd;

            State2nd = null;
            _handler2nd = null;
            Priority2nd = -1;

            if (h.on)
            h.OnMotorEnd (m);
        }

        public void OnSysEnd(sys s)
        {
            SceneMaster.Processor.FakeEnd (s);

            if ( s == State )
            End ();

            if ( s == State2nd )
            End2nd ();
        }
    }

}
