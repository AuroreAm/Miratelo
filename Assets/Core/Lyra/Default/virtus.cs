using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [InitializeWithSceneMaster]
    public class VirtualPoolMaster : dat
    {
        static VirtualPoolMaster o;

        Dictionary <int, virtus.VirtusPool > _pools;

        protected override void OnStructured()
        {
            o = this;
            _pools = new Dictionary<int, virtus.VirtusPool> ();

            VirtusAuthor [] authors = GameResources.Virtus.GetAll ();

            for (int i = 0; i < authors.Length; i++)
            {
                var pool = new virtus.VirtusPool ( authors [i] );
                _pools.Add (  new term (authors[i].name), pool );
            }
        }

        public static void AddPool ( IVirtusAuthor author, string name )
        {
            o._pools.Add ( new term (name), new virtus.VirtusPool (author) );
        }

        public static void RentVirtus ( int name )
        {
            o._pools [name].RentVirtus ();
        }
    }


    public sealed class virtus : action
    {
        VirtusPool _poolOrigin;
        List <Lyra.sys.ext> _virtusMembers = new List<Lyra.sys.ext>();

        void Register (Lyra.sys.ext p )
        {
            _virtusMembers.Add (p);
        }

        void OnRent ()
        {
            SceneMaster.Processor.Start (this);
            for (int i = 0; i < _virtusMembers.Count; i++)
            this.Link ( _virtusMembers [i] );
        }

        void OnReturn ()
        {
            Stop ();
        }

        public abstract class sys : Lyra.sys.ext
        {
            [Link]
            protected virtus Virtus;

            protected override void OnStructured()
            {
                Virtus.Register ( this );
                Create ();
            }

            protected virtual void Create () {}
        }

        public void Return_ ()
        {
            _poolOrigin.ReturnVirtus (this);
        }

        public class VirtusPool
        {
            Queue<virtus> _queue;
            List<virtus> PendingVirtus = new List<virtus>();

            IVirtusAuthor author;

            public VirtusPool(IVirtusAuthor author)
            {
                this.author = author;
                _queue = new Queue<virtus>();
            }

            public void RentVirtus()
            {
                CheckCapacity();

                virtus u = _queue.Dequeue();
                u.OnRent();
            }

            void CheckCapacity()
            {
                if (_queue.Count == 0)
                {
                    var v = author.Instance ();
                    v._poolOrigin = this;
                    _queue.Enqueue(v);
                }
            }

            int _currentFrame;
            public void ReturnVirtus(virtus v)
            {
                v.OnReturn();
                PendingVirtus.Add(v);
                _currentFrame = Time.frameCount;

                // to make sure the virtus is not used again in the same frame, they are moved to the pending list first then reused on a later frame
                if ( Time.frameCount != _currentFrame && PendingVirtus.Count > 0 )
                {
                    foreach (var p in PendingVirtus)
                        _queue.Enqueue(p);
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