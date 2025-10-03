using System.Collections.Generic;
using System;
using Lyra;
using UnityEngine;

namespace Lyra
{
    public abstract class task_decorator : task, core_kind, decorator_kind {
        protected sealed override void _ready() {
            for (int i = 0; i < o.Length; i++)
                system.add ( o[i] );

            __ready ();
        }

        public static task_decorator domain {protected set; get;}

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
        
        public void replace ( tasks t ) {
            _replace ( t );
        }

        public void replace_before ( tasks t ) {
            _replace_before ( t );
        }

        protected virtual void _task_fail () {}
        protected virtual void _replace ( tasks t ) {}
        protected virtual void _replace_before ( tasks t ) {}

        protected virtual void __ready () {}
    }
}