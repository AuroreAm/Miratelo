using System.Collections.Generic;
using System;

namespace Lyra
{
    public abstract class acting : task , decorator, core_kind, act_handler {
        public decorator.handle contract => core;
        protected decorator.core <action> core;
        action [] o => core.o;

        [link]
        motor motor;
        act act;
        parallel.all parallel;
        protected abstract act get_act();

        protected sealed override void _ready() {
            __ready ();
            act = get_act ();
        }

        protected override void _descend() {
            parallel.descend (this);
        }

        protected virtual void __ready () {}

        private acting () {
            core = new decorator.core<action> (this);
            core._set = set;
        }
        
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
        protected sealed override void _start() {
            motor_start_act ();

            if (!on)
            return;

            parallel.tick (this);
        }

        protected override bool _can_start() {
            return motor_can_start_act ();
        }

        protected sealed override void _step() {
            if (parallel.on)
                parallel.tick (this);
        }

        protected sealed override void _stop() {
            if (act.on)
            motor_stop_act ();

            if (parallel.on)
            parallel.abort (this);
        }

        protected sealed override void _abort() {
            _stop ();
        }

        void set(action[] child) {
            parallel = with ( new parallel.all () );
            parallel.contract.set_childs (child);
        }

        public void _star_stop(star s) {}
    }

    [path ("acting")]
    public class stop : action {
        protected override void _start() {
            look_for_acting_parent().stop_act ();
        }

        acting look_for_acting_parent () {
            acting result = null;
            for (int i = ancestors.Count - 1; i >= 0; i--)
            {
                if (ancestors[i] is acting a)
                {
                    result = a;
                    break;
                }
            }
            return result;
        }
    }
}
