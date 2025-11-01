using System;
using UnityEngine;

namespace Lyra {
    public class task_decorator : task, decorator, core_kind {
        public decorator.handle contract => core;
        protected decorator.core<task> core;
        protected task [] o => core.o;

        protected task_decorator () {
            core = new decorator.core<task>(this);
        }

        protected sealed override void _descend() {
            core._descend();
        }

        public virtual void _star_stop(star s) {
        }

        internal void task_failed() {
            _task_fail();
        }

        public void replace(tasks t) {
            _replace(t);
        }

        public void replace_before(tasks t) {
            _replace_before(t);
        }
        
        protected virtual void _task_fail() { }
        protected virtual void _replace(tasks t) { }
        protected virtual void _replace_before(tasks t) { }
    }
}