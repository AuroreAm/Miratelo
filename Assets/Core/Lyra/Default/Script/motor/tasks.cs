using Lyra;
using UnityEngine;

namespace Lyra {
    [path ("script")]
    public class tasks : task_decorator {
        task ptr_task;
        bool hold;
        int ptr;

        [export]
        public bool repeat = false;
        [export]
        public bool reset = true;
        [export]
        public bool absolute;

        public static tasks new_tasks ( action[] o ){
            set_constructor_event ( s => ((tasks)s).set ( o ) );
            return new tasks ();
        }

        protected sealed override void _start() {
            if (reset)
                ptr = 0;
            hold = false;

            ptr_task = o[ptr];

            ptr_task.tick (this);
        }

        protected override void _step() {
            if (!hold)
            ptr_task.tick (this);
            else
            check_hold ();
        }

        protected override void _stop() {
            if (ptr_task.on)
                ptr_task.abort(this);
        }

        public override void _star_stop(star s) {
            if (!on)
                return;

            if (ptr_task == s) {
                    ptr++;
                    if (ptr >= o.Length) { ptr = 0; 
                    if (!repeat) { 
                        if (hold) fail(); else stop(); 
                        return; } }

                ptr_task = o[ptr];
            }

            if (!hold)
            ptr_task.tick (this);
        }

        void check_hold () {
            if ( ptr_task.can_start () ) {
                hold = false;
                ptr_task.tick (this);
            }
        }

        protected override bool _can_start() {
            return o [0].can_start ();
        }
        
        protected override void _task_fail() {
            if (!absolute)
            hold = true;
            else { fail (); }
        }

        protected override void _substitute(tasks t) {
            var previous = ptr_task;
            ptr_task = t;
            previous.abort (this);
        }

        protected override void _insert_substitute(tasks t) {
            ptr --;
            _substitute(t);
        }
    }
}