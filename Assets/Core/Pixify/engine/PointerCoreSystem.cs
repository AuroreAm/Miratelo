using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class ModulePointer <T> : module where T : moduleptr <T>
    {
        static ModulePointer<T> o;
        protected Dictionary<int, T> ptr = new Dictionary<int, T> ();

        public ModulePointer ()
        {
            o = this;
        }

        public static void Register ( int id, T module )
        {
            if ( o!=null )
            o.ptr.Add ( id, module );
        }
    }

    public abstract class moduleptr <T> : module where T:moduleptr <T>
    {
        public sealed override void Create()
        {
            ModulePointer <T>.Register ( character.gameObject.GetInstanceID(), (T) this );
            Create1 ();
        }

        public virtual void Create1 ()
        {}
    }
}