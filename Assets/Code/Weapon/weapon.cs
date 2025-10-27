using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code {
    public abstract class weapon : moon {
        [link]
        character c;

        public abstract weapon_handle handle {get;}
        public Transform coord => c.gameobject.transform;
    }

    [need_ready]
    public abstract class weapon_handle : action {
        private weapon_handle() { }
        public warrior owner { get; protected set; }

        protected abstract i_interest_handler interest_handler { get; }

        protected sealed override void _ready() {
            dispose();
            __ready ();
        }

        public void aquire(warrior _owner) {
            if (!on) {
                Debug.LogError("Weapon already owned");
                return;
            }

            interest_handler.unregister();
            owner = _owner;
            stop();
        }

        public void dispose() {
            ready_for_tick();
            phoenix.core.start_action(this);
        }


        protected sealed override void _start() {
            interest_handler.register();
            owner = null;
            __start();
        }

        protected virtual void __ready () {}

        protected virtual void __start() { }

        public interface i_interest_handler {
            public void register();
            public void unregister();
            public weapon weapon {get;}
        }

        public abstract class free<T> : weapon_handle where T : weapon {
            [link]
            interest inter;

            protected sealed override i_interest_handler interest_handler => inter;

            public class interest : Code.interest, i_interest_handler {
                [link]
                character c;

                [link]
                T _weapon;

                public weapon weapon => _weapon;

                public override Vector3 pos => c.position;

                public void register() {
                    register(c.gameobject.GetInstanceID());
                }

                public void unregister() {
                    unregister(c.gameobject.GetInstanceID());
                }
            }

        }

    }


    [star(order.attack)]
    public abstract class attack : virtus.star { }
}