using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [superstar]
    public class orion : moon
    {
        static orion o;

        Dictionary <int, virtus.pool > _pools;

        protected override void _ready()
        {
            o = this;
            _pools = new Dictionary<int, virtus.pool> ();

            VirtusCreator [] authors = game_resources.virtus.get_all ();

            for (int i = 0; i < authors.Length; i++)
            {
                var pool = new virtus.pool ( authors [i] );
                _pools.Add (  new term (authors[i].name), pool );
            }
        }

        public static void AddPool ( virtus_creator author, string name )
        {
            o._pools.Add ( new term (name), new virtus.pool (author) );
        }

        public static void RentVirtus ( int name )
        {
            o._pools [name].rent_virtus ();
        }
    }


    public sealed class virtus : action
    {
        pool origin;
        List <main> satellites = new List<main>();

        void register (main p )
        {
            satellites.Add (p);
        }

        void _active ()
        {
            phoenix.core.start (this);
            for (int i = 0; i < satellites.Count; i++)
            this.link ( satellites [i] );
        }

        void _return ()
        {
            stop ();
        }

        public abstract class star : main
        {
            [link]
            protected virtus virtus;

            protected override void _ready()
            {
                virtus.register ( this );
                __ready ();
            }

            protected virtual void __ready () {}
        }

        public void return_ ()
        {
            origin.return_virtus (this);
        }

        public class pool
        {
            Queue<virtus> queue;
            List<virtus> pending = new List<virtus>();

            virtus_creator author;

            public pool(virtus_creator _author)
            {
                author = _author;
                queue = new Queue<virtus>();
            }

            public void rent_virtus ()
            {
                prepare_capacity ();

                virtus u = queue.Dequeue();
                u._active();
            }

            void prepare_capacity ()
            {
                if (queue.Count == 0)
                {
                    var v = author.instance ();
                    v.origin = this;
                    queue.Enqueue(v);
                }
            }

            int frame;
            public void return_virtus (virtus v)
            {
                v._return();
                pending.Add(v);
                frame = Time.frameCount;

                // to make sure the virtus is not used again in the same frame, they are moved to the pending list first then reused on a later frame
                if ( Time.frameCount != frame && pending.Count > 0 )
                {
                    foreach (var p in pending)
                        queue.Enqueue(p);
                    pending.Clear();
                }}
        }
    }

}