using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class thing : node
    {}

    public abstract class ThingPointer <T> : module where T : thingptr <T>
    {
        static ThingPointer<T> o;
        protected Dictionary<int, T> ptr = new Dictionary<int, T> ();

        public ThingPointer ()
        {
            o = this;
        }

        public static void Register ( int id, T thing )
        {
            if ( o!=null )
            o.ptr.Add ( id, thing );
        }
    }

    public abstract class thingptr <T> : thing where T : thingptr<T>
    {
        public void Create ( int instanceID )
        {
            ThingPointer <T>.Register ( instanceID, (T) this );
        }
    }
}
