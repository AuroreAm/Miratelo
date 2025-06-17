using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class ModulePointerSystem <T> : PixifySytemBase where T : moduleptr <T>
    {
        static ModulePointerSystem<T> o;
        protected Dictionary<int, T> ptr = new Dictionary<int, T> ();

        public ModulePointerSystem ()
        {
            o = this;
        }

        public static void Register ( int id, T module )
        {
            if ( o!=null )
            o.ptr.Add ( id, module );
        }
    }

    public class moduleptr <T> : module where T:moduleptr <T>
    {
        public sealed override void Create()
        {
            ModulePointerSystem <T>.Register ( character.gameObject.GetInstanceID(), (T) this );
        }
    }
}