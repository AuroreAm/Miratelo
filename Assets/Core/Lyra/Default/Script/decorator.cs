using System;
using System.Collections.Generic;

namespace Lyra {
    public interface decorator {
        public abstract decorator.handle contract { get; }
        public interface handle { 
            public void set_childs ( action [] childs );
            public action [] get_childs ();
        }

        public sealed class core<T> : handle where T : action {
            public T[] o;
            action host;
            public Action <T[]> _set;

            public core(action _host) {
                host = _host;
            }

            public void _descend() {
                for (int i = 0; i < o.Length; i++)
                    o[i].descend(host);
            }

            public void set_childs ( action [] childs ) {
                List <T> childs_ = new List<T> ();
                for (int i = 0; i < childs.Length; i++)
                {
                    if ( childs[i] is T a)
                    childs_.Add (a);
                    else
                    throw new InvalidOperationException("icompatible decorator child");
                }
                set ( childs_.ToArray () );
            }

            public action [] get_childs () {
                List <T> childs_ = new List<T> ();
                for (int i = 0; i < o.Length; i++)
                {
                    childs_.Add ( o[i] );
                }
                return childs_.ToArray ();
            }

            public void set(T[] child) {
                if (host.on)
                    throw new InvalidOperationException("can't set active decorator");

                o = child;

                _set?.Invoke ( child );
            }

            public void radiate<T1>(T1 gleam) where T1 : struct {
                foreach (var child in o) {
                    if ((child as ruby<T1>) != null) {
                        (child as ruby<T1>)._radiate(gleam);
                        return;
                    }
                }
            }
        }
    }

    public class action_decorator : action, decorator, core_kind {
        public decorator.handle contract => core;
        protected decorator.core <action> core;
        protected action [] o => core.o;

        protected action_decorator () {
            core = new decorator.core<action> (this);
        }

        protected sealed override void _descend() {
            core._descend ();
        }

        public virtual void _star_stop ( star s ) {
        }
    }

    /*
    public interface decorator_kind {
        public void set ( action [] child );
    }

    [path ("decorator")]
    public abstract class decorator : action, core_kind, decorator_kind
    {
        protected action [] o;
        public abstract void _star_stop(star s);

        public void set ( action [] child )
        {
            if ( on )
            throw new InvalidOperationException ( "can't set active decorator" );

            o = child;
        }

        protected sealed override void _descend() {
            for (int i = 0; i < o.Length; i++) {
                o [i].descend (this);
            }
        }

        public virtual void radiate <T> ( T gleam ) where T : struct {
            foreach ( var child in o ) {
                if ( (child as ruby <T>) != null ) {
                    (child as ruby <T>)._radiate ( gleam );
                    return;
                }
            }

            
        }
    }*/
}