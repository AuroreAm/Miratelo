using System.Collections.Generic;

namespace Lyra
{
    public class DatIndex <T> : dat where T : dat_indexed <T>
    {
        static DatIndex <T> o;
        protected Dictionary <int, T> ptr = new Dictionary<int, T> ();

        public DatIndex ()
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

    public abstract class dat_indexed <T> : dat where T : dat_indexed <T>
    {
        public void Register ( int id )
        {
            DatIndex <T>.Register (id, (T) this);
        }
    }

    public abstract class dat_character_indexed <T> : dat_indexed <T> where T:dat_character_indexed <T>
    {
        [Link]
        protected character c;

        protected sealed override void OnStructured()
        {
            DatIndex <T>.Register ( c.GameObject.GetInstanceID (), (T) this );
            Create ();
        }

        protected virtual void Create ()
        {}
    }
}