using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Lyra {
    [path ("script")]
    public class tasks : task_decorator {
        [export]
        public bool repeat = false;
        [export]
        public bool reset = true;
        [export]
        public bool absolute;

        int ptr;
        action ptr_task;

        int state;
        const int sequence = 0; const int replaced = 1; const int replacing = 2; const int hold = 3;

        void tick () {
            domain = this;
            ptr_task.tick (this);
        }

        protected sealed override void _start() {
            state = sequence;

            if (reset)
            ptr = 0;

            ptr_task = o [ptr];
            tick ();
        }

        protected override void _step() {
            if (state != hold)
            tick ();
            else check_continue ();
        }

        void check_continue () {
            if ( o [ptr + 1].can_start () ) {
            state = sequence;
            increment ();
            }
        }

        protected override void _stop() {
            if (ptr_task.on)
            ptr_task.abort (this);
        }

        public override void _star_stop(star s) {
            if (!on)
                return;

            if (state == sequence) {
                if (finished ()) {
                    stop ();
                    return;
                }

                increment ();
            }
            else if (state == replaced) {
                if (finished ()) {
                    stop ();
                    return;
                }

                state = sequence;
                increment ();
            }
        }

        void increment () {
             ptr++;
             ptr_task = o [ptr];
             tick ();
        }

        bool finished () => ptr +1 > o.Length && !repeat;

        protected override bool _can_start() {
            return o [0].can_start ();
        }
        
        protected override void _task_fail() {
            if ( finished () ) {
                fail ();
                return;
            }
            if (state == sequence)
            ptr ++;
            state = hold;
        }

        protected override void _replace(tasks t) {
            if ( state == sequence ) {
                state = replacing;
                ptr_task.abort (this);
                ptr_task = t;
                state = replaced;
                tick ();
            } else if ( state == replaced ) {
                Dev.Break ( "can't replace task twice" );
            }
        }

        protected override void _replace_before(tasks t) {
            if (state == sequence) {
                ptr --;
                _replace ( t );
            }
        }
    }
}