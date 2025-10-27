using System.Collections.Generic;

namespace Lyra
{
    public abstract class index <T> : moon where T : public_moon <T>
    {
        static index <T> o;
        protected Dictionary <int, T> ptr = new Dictionary<int, T> ();

        public index ()
        {
            o = this;
        }

        public static bool contains ( int id )
        {
            return o.ptr.ContainsKey ( id );
        }

        public static void register (int id, T pix)
        {
            o.ptr.Add ( id, pix );
            o._new (id, pix);
        }

        public static void unregister ( int id ) {
            var item = o.ptr [id];
            o.ptr.Remove ( id );
            o._exit (item);
        }

        protected virtual void _new ( int id, T pix ) {}
        protected virtual void _exit ( T pix ) {}
    }

    public abstract class public_moon <T> : moon where T : public_moon <T>
    {
        public void register ( int id )
        {
            index <T>.register (id, (T) this);
        }

        public void unregister ( int id ) {
            index <T>.unregister (id);
        }

        public abstract class of_character <T1> : public_moon <T1> where T1:of_character <T1>
        {
            [link]
            protected character c;

            protected sealed override void _ready()
            {
                index <T1>.register ( c.gameobject.GetInstanceID (), (T1) this );
                __ready ();
            }

            protected virtual void __ready ()
            {}
        }
    }

}