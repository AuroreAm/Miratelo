using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{

    public class VirtualPoolMaster : pix
    {
        static VirtualPoolMaster o;

        Dictionary <int, virtus.VirtusPool > Pools;

        public override void Create()
        {
            o = this;
            Pools = new Dictionary<int, virtus.VirtusPool> ();

            VirtusAuthor [] authors = SubResources <VirtusAuthor>.GetAll ();

            for (int i = 0; i < authors.Length; i++)
            {
                var pool = new virtus.VirtusPool ( authors [i] );
                Pools.Add (  new term (authors[i].name), pool );
            }
        }

        public static void AddPool ( IVirtusAuthor author, string name )
        {
            o.Pools.Add ( new term (name), new virtus.VirtusPool (author) );
        }

        public static void RentVirtus ( int name )
        {
            o.Pools [name].RentVirtus ();
        }
    }


    // module for highly reusable block
    public sealed class virtus : pix
    {
        VirtusPool PoolOrigin;
        List <pixi.managed> virtual_pix = new List<pixi.managed> ();
        int [] keys = new int [1];

        void Register ( pixi.managed p )
        {
            virtual_pix.Add (p);
        }

        void Start ()
        {
            if ( keys.Length != virtual_pix.Count )
            keys = new int [virtual_pix.Count];

            for (int i = 0; i < keys.Length; i++)
            keys [i] = Stage.Start ( virtual_pix [i] );
        }

        void Free ()
        {
            for (int i = 0; i < keys.Length; i++)
            Stage.Stop ( keys [i] );
        }

        public abstract class pixi : Pixify.pixi.managed
        {
            [Depend]
            protected virtus v;

            public sealed override void Create()
            {
                v.Register ( this );
                Create1 ();
            }

            protected virtual void Create1 () {}
        }

        public void Return_ ()
        {
            PoolOrigin.ReturnVirtus (this);
        }

        public class VirtusPool
        {
            Queue<virtus> queue;
            List<virtus> PendingVirtus = new List<virtus>();
            int currentFrame;

            IVirtusAuthor author;

            public VirtusPool(IVirtusAuthor author)
            {
                this.author = author;
                queue = new Queue<virtus>();
            }

            public void RentVirtus()
            {
                CheckCapacity();

                var u = queue.Dequeue();
                u.Start();
            }

            void CheckCapacity()
            {
                if (queue.Count == 0)
                {
                    var v = author.Instance();
                    v.PoolOrigin = this;
                    queue.Enqueue(v);
                }
            }

            public void ReturnVirtus(virtus v)
            {
                v.Free();
                PendingVirtus.Add(v);
                currentFrame = Time.frameCount;

                // to make sure the virtus is not used again in the same frame, they are moved to the pending list first then reused on a later frame
                if ( Time.frameCount != currentFrame && PendingVirtus.Count > 0 )
                {
                    foreach (var p in PendingVirtus)
                        queue.Enqueue(p);
                    PendingVirtus.Clear();
                }
            }
        }
    }

    [Serializable]
    public struct PieceSkin
    {
        public Vector3 RotY;
        public Mesh Mesh;
        public Material Material;
    }

}