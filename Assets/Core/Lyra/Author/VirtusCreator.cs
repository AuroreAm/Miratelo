using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public abstract class VirtusCreator : ScriptableObject, creator, virtus_creator {
        public virtus instance() {
            var b = new system.creator(this).create_system();
            return b.get<virtus>();
        }

        public void _create() {
            new ink<virtus>();
            _virtus_create();
        }

        public void _created(system s) { }
        protected abstract void _virtus_create();
    }

    public abstract class VirtusCreator<T> : VirtusCreator where T : bridge, new() {
        protected T bridge_cache(ref T w) {
            if (w == null) {
                var n = new term(name);
                w = bridge.create<T>(n);
                orion.add(this, name);
            }

            return w;
        }

        T w;
        public T get_w() => bridge_cache(ref w);
    }


    public abstract class virtus_creator_simple : creator, virtus_creator {
        public virtus instance() {
            var b = new system.creator(this).create_system();
            return b.get<virtus>();
        }

        public void _create() {
            new ink<virtus>();
            _virtus_create();
        }

        public void _created(system s) { }

        protected abstract void _virtus_create();
    }

    public abstract class bridge {
        protected int name;

        public static T create <T> ( int name ) where T : bridge, new() {
            return new T() { name = name };
        }
    }

    public abstract class virtus_creator_simple<T> : virtus_creator_simple where T : bridge, new() {
        public abstract string name {get;}
        protected T bridge_cache(ref T w) {
            if (w == null) {
                var n = new term (name);
                w = bridge.create<T>(n);
                orion.add(this, name);
            }

            return w;
        }

        T w;
        public T get_w() => bridge_cache(ref w);
    }

    public interface virtus_creator {
        public virtus instance();
    }
}
