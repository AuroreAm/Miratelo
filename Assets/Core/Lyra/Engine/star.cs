using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Lyra {
    public abstract class star : moon {
        public bool on { private set; get; }
        private core_kind core;

        public star() {
            need_ready = GetType().GetCustomAttribute<need_readyAttribute>() != null;
        }

        /// <summary>  ready for tick </summary>
        public bool ready { private set; get; }
        bool need_ready;

        private List<star> web = new List<star>();
        private star host;

        protected virtual void _start() { }

        protected virtual void _step() { }

        protected virtual void _stop() { }

        protected virtual void _abort() {
            _stop();
        }

        protected void link(star.main linked) {
            if (!on) {
                Debug.LogError ("star is not on");
                return;
            }

            if (linked.host != null) {
                Debug.LogError ("star already has a host");
                return;
            }

            linked.host = this;
            web.Add(linked);
            phoenix.core.link(linked);
        }

        protected void reverse_link (star _host) {
            if (!on) {
                Debug.LogError ("star is not on");
                return;
            }

            if (host != null) {
                Debug.LogError ("star already has a host");
                return;
            }

            if (!_host.on)
            {
                Debug.LogError ("host is not on");
                return;
            }

            host = _host;
            host.web.Add (this);
        }

        protected void unlink(star.main linked) {
            linked.host = null;
            web.Remove(linked);
            linked.stop();
        }

        void clear_web() {
            foreach (star link in web) {
                link.host = null;
                link.stop();
            }

            web.Clear();

            if ( host!=null ) {
                host.web.Remove(this);
                host = null;
            }
        }

        private void tick() {
            if (!ready && need_ready) {
                Dev.Break($"star {this.GetType().Name} is not ready for execution");
                return;
            }

            if (!on) {
                on = true;

                enter_my_system_field();
                _start();
                exit_my_system_field();

                return;
            }
            if (on) {
                enter_my_system_field();
                _step();
                exit_my_system_field();
            }
        }

        private void abort() {
            if (on) {
                on = false;

                enter_my_system_field();
                _abort();
                ready = false;
                clear_web();
                exit_my_system_field();

                notify_core_on_stopping();
            }
            else
                Dev.Break($"abort of stopped {this.GetType()}");
        }

        private void stop() {
            if (on) {
                on = false;

                enter_my_system_field();
                _stop();
                ready = false;
                clear_web();
                exit_my_system_field();
                notify_core_on_stopping();
            }
            else
                Dev.Break($"double stop of {this.GetType()}");
        }

        protected void ready_for_tick() {
            if (!on)
                ready = true;
            else
                Dev.Break("star is already on");
        }

        // TODO: maybe ready_for_tick called but not immediately ticked after

        public void tick(core_kind _core) {
            if (on == false)
                core = _core;


            if (_core != core)
                Dev.Break($"{this.GetType()} can't be ticked by corekind {_core} as this is not the original ticker");

            tick();
        }

        void notify_core_on_stopping() {
            var c = core;
            core = null;
            c._star_stop(this);
        }

        public void stop(core_kind handler) {
            if (handler == core)
                abort();
            else
                Dev.Break($"{this.GetType()} can't be stopped by the corekind {handler} as this is not the original ticker");
        }

        public abstract class main : star {
        }

        public abstract class neutron : star {
            /// <summary> make sure anything after stop() is suposed to run even the star is stopped </summary>
            protected new void stop() {
                base.stop();
            }
        }
    }

    public interface core_kind {
        /// <summary> called when a ticked star stops, careful when the ticker is another star, it will always call </summary>
        public void _star_stop(star s);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class need_readyAttribute : Attribute { }
}