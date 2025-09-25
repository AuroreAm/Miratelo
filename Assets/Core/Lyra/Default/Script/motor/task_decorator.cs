using System.Collections.Generic;
using System;
using Lyra;
using UnityEngine;

namespace Lyra
{
    public abstract class task_decorator : task, core_kind, decorator_kind {
        protected static Stack<task_decorator> static_domain = new Stack<task_decorator> ();

        protected void task_tick ( action o ) {
            static_domain.Push (this);
            o.tick ( this );
            static_domain.Pop ();
        }

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

        protected sealed override void _ready() {
            for (int i = 0; i < o.Length; i++)
                system.add ( o[i] ) ;

            __ready ();
        }

        internal static void task_failed () {
            if (static_domain.Count == 0) {
                Debug.LogError (" task_failed called outside task decorator");
                return;
            }

            static_domain.Peek ()._task_fail ();
        }
        
        public static void substitute ( tasks t ) {
            if (static_domain.Count == 0) {
                Debug.LogError (" substitute called outside task decorator");
                return;
            }

            static_domain.Peek ()._substitute ( t );
        }

        public static void insert_substitute ( tasks t ) {
            if (static_domain.Count == 0) {
                Debug.LogError (" insert_substitute called outside task decorator");
                return;
            }

            static_domain.Peek ()._insert_substitute ( t );
        }

        protected virtual void _task_fail () {}
        protected virtual void _substitute ( tasks t ) {}
        protected virtual void _insert_substitute ( tasks t ) {}

        protected virtual void __ready () {}
    }
}