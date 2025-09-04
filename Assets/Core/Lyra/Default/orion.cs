using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{

    // constelation pool

    [lead]
    public class orion : shard
    {
        static orion o;

        Dictionary <int, dot.pool > _pools;

        protected override void harmony()
        {
            o = this;
            _pools = new Dictionary<int, dot.pool> ();

            DotAuthor [] authors = Pile.Dot.radiate ();

            for (int i = 0; i < authors.Length; i++)
            {
                var pool = new dot.pool ( authors [i] );
                _pools.Add (  new term (authors[i].name), pool );
            }
        }

        public static void orbit ( IDotAuthor author, string name )
        {
            o._pools.Add ( new term (name), new dot.pool (author) );
        }

        public static void dot ( int name )
        {
            o._pools [name].RentVirtus ();
        }
    }


    public sealed class dot : act
    {
        pool origin;
        List <flow> members = new List<flow>();

        void register (flow p )
        {
            members.Add (p);
        }

        void InRent ()
        {
            phoenix.core.start (this);
            for (int i = 0; i < members.Count; i++)
            this.link ( members [i] );
        }

        void InReturn ()
        {
            sleep ();
        }

        public abstract class aria : flow
        {
            [harmony]
            protected dot dot;

            protected override void harmony()
            {
                dot.register ( this );
                Create ();
            }

            protected virtual void Create () {}
        }

        public void Return_ ()
        {
            origin.ReturnVirtus (this);
        }

        public class pool
        {
            Queue<dot> queue;
            List<dot> pending = new List<dot>();

            IDotAuthor author;

            public pool(IDotAuthor author)
            {
                this.author = author;
                queue = new Queue<dot>();
            }

            public void RentVirtus()
            {
                CheckCapacity();

                dot u = queue.Dequeue();
                u.InRent();
            }

            void CheckCapacity()
            {
                if (queue.Count == 0)
                {
                    var v = author.Instance ();
                    v.origin = this;
                    queue.Enqueue(v);
                }
            }

            int photo;
            public void ReturnVirtus(dot v)
            {
                v.InReturn();
                pending.Add(v);
                photo = Time.frameCount;

                // to make sure the dot is not used again in the same frame, they are moved to the pending list first then reused on a later frame
                if ( Time.frameCount != photo && pending.Count > 0 )
                {
                    foreach (var p in pending)
                        queue.Enqueue(p);
                    pending.Clear();
                }
            }
        }
    }
}