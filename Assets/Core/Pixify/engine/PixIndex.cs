using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class PixIndex <T> : pix where T : indexed_pix <T>
    {
        static PixIndex <T> o;
        protected Dictionary <int, T> ptr = new Dictionary<int, T> ();

        public PixIndex ()
        {
            o = this;
        }

        public static bool Contains ( int id )
        {
            return o.ptr.ContainsKey ( id );
        }

        public static void Register (int id, T pix)
        {
            o.ptr.Add ( id, pix );
        }
    }

    public abstract class indexed_pix <T> : pix where T : indexed_pix <T>
    {
        public void Register ( int id )
        {
            PixIndex <T>.Register (id, (T) this);
        }
    }

    public abstract class character_indexed_pix <T> : indexed_pix <T> where T:character_indexed_pix <T>
    {
        [Depend]
        protected character c;

        public sealed override void Create()
        {
            PixIndex <T>.Register ( c.gameObject.GetInstanceID (), (T) this );
            Create1 ();
        }

        public virtual void Create1 ()
        {}
    }
}