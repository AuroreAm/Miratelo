using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    /*public class task_sequence : decorator
    {
        action ptr_action;
        int ptr;

        bool resume_after;

        [link]
        script script;

        [export]
        public bool repeat = true ;
        [export]
        public bool reset = true ;

        protected sealed override void _start ()
        {
            if (reset)
            ptr = 0;
            ptr_action = o [ptr];

            tick_ptr_action ();
        }

        protected sealed override void _step ()
        {
            tick_ptr_action ();
        }

        protected override void _stop()
        {
            if ( ptr_action.on )
            ptr_action.stop (this);
        }

        public static void substitute ( term term, bool resume_after = false )
        {
            if (static_domain.Count == 0)
            throw new InvalidOperationException ( "can't use task_sequence outside of its child" );
            
            static_domain.Peek ().substitute_internal ( static_domain.Peek ().script [term], resume_after );
        }
        static Stack <task_sequence> static_domain = new Stack<task_sequence> () ;
        void substitute_internal ( action action, bool resume_after )
        {
            if (!on)
            return;

            if ( resume_after == false )
            this.resume_after = resume_after;
            
            var previous = ptr_action;
            ptr_action = action;
            previous.stop (this);
        }

        public static void abort ()
        {
            if (static_domain.Count == 0)
            throw new InvalidOperationException ( "can't use task_sequence outside of its child" );
            
            static_domain.Peek ().stop ();
        }
        
        void tick_ptr_action ()
        {
            static_domain.Push (this);
            ptr_action.tick (this );
            static_domain.Pop ();
        }

        public override void _star_stop(star p)
        {
            if (!on)
            return;

            if ( ptr_action == p )
            {
                if (resume_after)
                    resume_after = false;
                else
                {
                    ptr++;
                    if (ptr >= o.Length)
                    {
                        ptr = 0;
                        if (!repeat)
                        {
                            stop();
                            return;
                        }
                    }
                }
                ptr_action = o [ptr];
            }

            tick_ptr_action ();
        }
    }

    [path("")]
    public sealed class index : task_sequence
    {
        public term name {private set; get;}

        public index (string _name)
        {
            name = new term (_name);
        }
    }
    

    [path ("script")]
    public sealed class substitute : action
    {
        [export]
        public term term;

        protected override void _start()
        {
            task_sequence.substitute ( term );
        }
    }*/

}