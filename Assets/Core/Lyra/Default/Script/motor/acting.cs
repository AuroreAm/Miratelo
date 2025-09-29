using System.Collections.Generic;
using System;

namespace Lyra
{
    public abstract class acting : task , decorator_kind, core_kind, act_handler {
        [link]
        motor motor;

        act act;

        action [] o;
        parallel.all parallel;
        protected abstract act get_act();

        static Stack <acting> domain = new Stack <acting> ();
        protected sealed override void _ready() {
            domain.Push (this);
            system.add ( parallel );
            __ready ();
            domain.Pop ();

            act = get_act ();
        }
        public static acting get_domain () => domain.Peek ();

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

        public void stop_act () {
            motor_stop_act ();
        }

        // TODO bool can start
        protected override void _start() {
            motor_start_act ();

            if (!on)
            return;

            parallel.tick (this);
        }

        protected override bool _can_start() {
            return motor_can_start_act ();
        }

        protected override void _step() {
            if (parallel.on)
                parallel.tick (this);
        }

        protected override void _stop() {
            if (act.on)
            motor_stop_act ();

            if (parallel.on)
            parallel.abort (this);
        }

        public void set(action[] child) {
            o = child;
            parallel = new parallel.all ();
            parallel.set (child);
        }

        public void _star_stop(star s) {}
    }

    [path ("acting")]
    public class stop : action {
        acting domain;

        protected override void _ready() {
            domain = acting.get_domain ();
        }

        protected override void _start() {
            domain.stop_act ();
        }
    }
}
