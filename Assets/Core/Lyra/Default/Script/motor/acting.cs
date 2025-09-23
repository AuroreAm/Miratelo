using System.Collections.Generic;
using System;

namespace Lyra
{
    public abstract class acting : parallel, act_handler {
        static Stack <acting> static_domain = new Stack <acting> ();

        [link]
        motor motor;

        act act;

        protected abstract act get_act();

        protected sealed override void __ready() {
            ___ready ();
            act = get_act ();
        }

        protected virtual void ___ready () {}

        // NOTE if starting was failed, the action stops, need to check on before continuing operation after stop
        // TODO task failed
        public void _act_end( act a, act_status status ) {
            if ( on && a == act )
            stop ();
        }

        public override void _star_stop(star s) {}

        public static void stop_act () {
            if (static_domain.Count == 0)
            throw new InvalidOperationException ( "can't use acting outside of its child" );
            
            static_domain.Peek ().motor.stop_act ( static_domain.Peek () );
        }

        // TODO bool can start
        protected override void _start() {
            motor.start_act ( act, this );

            if (!on)
            return;

            static_domain.Push (this);
            base._start();
            static_domain.Pop ();
        }

        protected override void _step() {
            static_domain.Push (this);
            base._step();
            static_domain.Pop ();
        }
    }

    [path ("acting")]
    public class stop : action {
        protected override void _start() {
            acting.stop_act ();
        }
    }
}
