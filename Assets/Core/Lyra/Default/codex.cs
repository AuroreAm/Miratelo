using System.Collections.Generic;

namespace Lyra
{
    // shard with registry
    
    public class codex <T> : shard where T : arcanum <T>
    {
        static codex <T> o;
        protected Dictionary <int, T> ptr = new Dictionary<int, T> ();

        public codex ()
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

    public abstract class arcanum <T> : shard where T : arcanum <T>
    {
        public void Register ( int id )
        {
            codex <T>.Register (id, (T) this);
        }
    }

    public abstract class aspect <T> : arcanum <T> where T:aspect <T>
    {
        [harmony]
        protected character c;

        protected sealed override void harmony()
        {
            codex <T>.Register ( c.form.GetInstanceID (), (T) this );
            convergence1 ();
        }

        protected virtual void convergence1 ()
        {}
    }
}