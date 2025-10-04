using System;

namespace Lyra
{
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

            if ( system != null )
            throw new InvalidOperationException ( "can't set already running decorator" );

            o = child;
        }

        protected sealed override void _ready()
        {
            for (int i = 0; i < o.Length; i++) {
                system.add ( o[i] ) ;
            }

            __ready ();
        }

        protected override void _descend() {
            for (int i = 0; i < o.Length; i++) {
                o [i].descend (this);
            }
        }

        protected virtual void __ready ()
        {}
    }
}