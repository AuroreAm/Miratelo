using UnityEngine;

namespace Lyra
{
    // motor layer of behavior
    // asign only state that directly manipulate motion of a character: animation and movement
    public class kinesis : will, ICore
    {
        ILucid handler;
        
        ILucid handler2nd;

        public motor state { get; private set; }
        public int priority {get; private set;} = -1;
        public bool accept2nd {get; private set;} = false;
        public motor state2nd { get; private set; }
        public int priority2nd {get; private set;} = -1;

        protected override void harmony()
        {
            phoenix.core.start (this);
        }

        protected override void alive()
        {
            if (state != null && state.on)
                state.sing(this);

            if (state2nd != null && state2nd.on)
                state2nd.sing(this);
        }

        public bool perform ( motor state, ILucid handler )
        {
            if (state.priority <= priority) return false;

            if (this.state != null)
            this.state.halt (this);

            this.state = state;
            priority = this.state.priority;
            accept2nd = this.state.accept2nd;

            this.handler = handler;

            if (!accept2nd && state2nd != null)
                state2nd.halt (this);

            phoenix.core.fakestart (this.state);
            this.state.sing (this);
            return true;
        }

        public void end ( ILucid handler )
        {
            if ( handler == this.handler )
                state.halt (this);
            else
                Debug.LogError (" handler can't stop state ");
        }

        public bool perform2nd ( motor state2nd, ILucid handler )
        {
            if (!accept2nd) return false;
            if (state2nd.priority <= priority2nd) return false;
            
            if (this.state2nd != null)
                this.state2nd.halt (this);

            handler2nd = handler;

            this.state2nd = state2nd;
            priority2nd = state2nd.priority;

            phoenix.core.fakestart (this.state2nd);
            this.state2nd.sing (this);
            return true;
        }

        public void end2nd ( ILucid handler )
        {
            if ( handler == handler2nd )
            state2nd.halt (this);
            else
            Debug.LogError (" handler can't stop state ");
        }

        void _end ()
        {
            var m = state;
            var h = handler;

            state = null;
            handler = null;
            priority = -1;
            
            accept2nd = false;

            if (h.on)
            h.inhalt (m);
        }

        void _end2nd ()
        {
            var m =state2nd;
            var h = handler2nd;

            state2nd = null;
            handler2nd = null;
            priority2nd = -1;

            if (h.on)
            h.inhalt (m);
        }

        public void inhalt(aria s)
        {
            phoenix.core.fakestop (s);

            if ( s == state )
            _end ();

            if ( s == state2nd )
            _end2nd ();
        }
    }

}
