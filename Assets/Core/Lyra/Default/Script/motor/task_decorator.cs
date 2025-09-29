using System.Collections.Generic;
using System;
using Lyra;
using UnityEngine;

namespace Lyra
{
    public abstract class task_decorator : task, core_kind, decorator_kind {

        static Stack<task_decorator> domain = new Stack<task_decorator> ();
        protected sealed override void _ready() {
            domain.Push (this);
            for (int i = 0; i < o.Length; i++)
                system.add ( o[i] );
            domain.Pop ();

            __ready ();
        }
        /// <summary> get the current task domain, only on ready </summary>
        public static task_decorator get_domain () => domain.Peek ();

        protected task [] o;
        public abstract void _star_stop(star s);

        public void set ( action [] child ) {
            if ( on )
            throw new InvalidOperationException ( "can't set active decorator" );

            if ( system != null )
            throw new InvalidOperationException ( "can't set already running decorator" );

            o = new task[child.Length];

            for (int i = 0; i < child.Length; i++)
                if (child[i] is task t)
                o[i] = t;
                else Debug.LogWarning ($"direct child of task decorator {child[i].GetType()} is not a task");
        }


        internal void task_failed () {   
            _task_fail ();
        }
        
        public void substitute ( tasks t ) {
            _substitute ( t );
        }

        public void insert_substitute ( tasks t ) {
            _insert_substitute ( t );
        }

        protected virtual void _task_fail () {}
        protected virtual void _substitute ( tasks t ) {}
        protected virtual void _insert_substitute ( tasks t ) {}

        protected virtual void __ready () {}
    }
}