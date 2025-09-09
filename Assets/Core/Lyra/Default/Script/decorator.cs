using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [path ("decorator")]
    public abstract class decorator : action, core_kind
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
            for (int i = 0; i < o.Length; i++)
             system.add ( o[i] ) ;
        }
    }
}