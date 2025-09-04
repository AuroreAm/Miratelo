using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [Path ("decorator")]
    public abstract class decorator : action, ISysProcessor
    {
        protected action [] o;
        public abstract void OnSysEnd(sys s);

        public void SetChild ( action [] child )
        {
            if ( on )
            throw new InvalidOperationException ( "can't set child of active decorator" );

            if ( Structure != null )
            throw new InvalidOperationException ( "can't set child of already structured decorator" );

            o = child;
        }

        protected sealed override void OnStructured()
        {
            for (int i = 0; i < o.Length; i++)
             Structure.Add ( o[i] ) ;
        }
    }
}