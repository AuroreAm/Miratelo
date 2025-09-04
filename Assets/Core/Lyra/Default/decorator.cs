using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [verse ("decorator")]
    public abstract class decorator : act, ICore
    {
        protected Lyra.act [] o;
        public abstract void inhalt(aria s);

        public void leaf (Lyra.act [] leaves )
        {
            if ( on )
            throw new InvalidOperationException ( "can't set leaf of active decorator" );

            if ( sky != null )
            throw new InvalidOperationException ( "can't set leaf of already structured decorator" );

            o = leaves;
        }

        protected sealed override void harmony()
        {
            for (int i = 0; i < o.Length; i++)
             sky.add ( o[i] ) ;
        }
    }
}