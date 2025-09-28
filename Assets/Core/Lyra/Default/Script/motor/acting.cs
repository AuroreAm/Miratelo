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

        private acting () {}
        protected abstract void motor_stop_act ();
        protected abstract void motor_start_act ();
        protected abstract bool motor_can_start_act ();

        public abstract class first : acting {
            override sealed protected bool motor_can_start_act() {
                return motor.can_start_act ( act );
            }
            protected sealed override void motor_start_act() {
                motor.start_act ( act, this );
            }
            protected sealed override void motor_stop_act() {
                motor.stop_act ( this );
            }
        }

        public abstract class second : acting {
            override sealed protected bool motor_can_start_act() {
                return motor.can_start_act2nd ( act );
            }
            protected sealed override void motor_start_act() {
                motor.start_act2nd ( act, this );
            }
            protected sealed override void motor_stop_act() {
                motor.stop_act2nd ( this );
            }
        }

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
            
            static_domain.Peek ().motor_stop_act ();
        }

        // TODO bool can start
        protected override void _start() {
            motor_start_act ();

            if (!on)
            return;

            static_domain.Push (this);
            parallel.tick (this);
            static_domain.Pop ();
        }

        protected override bool _can_start() {
            return motor_can_start_act ();
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
            motor_stop_act ();

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
