using System.Collections.Generic;
using System;

namespace Lyra
{
    public abstract class acting : task , decorator_kind, core_kind, act_handler {
        static Stack <acting> static_domain = new Stack <acting> ();

        [link]
        motor motor;

        act act;

        parallel.all parallel;
        protected abstract act get_act();

        protected sealed override void _ready() {
            system.add ( parallel );
            __ready ();
            act = get_act ();
        }

        protected virtual void __ready () {}

        // NOTE if starting was failed, the action stops, need to check on before continuing operation after stop
        // TODO task failed
        public void _act_end( act a, act_status status ) {
            if ( !on )
            return;

            if (status == act_status.done )
            stop ();
            else
            fail ();
        }

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
            parallel.tick (this);
            static_domain.Pop ();
        }

        protected override bool _can_start() {
            return motor.can_start_act ( act );
        }

        protected override void _step() {
            if (parallel.on) {
                static_domain.Push (this);
                parallel.tick (this);
                static_domain.Pop ();
            }
        }

        protected override void _stop() {
            if (act.on)
            motor.stop_act (this);

            if (parallel.on)
            parallel.abort (this);
        }

        public void set(action[] child) {
            parallel = new parallel.all ();
            parallel.set (child);
        }

        public void _star_stop(star s) {}
    }

    [path ("acting")]
    public class stop : action {
        protected override void _start() {
            acting.stop_act ();
        }
    }
}
