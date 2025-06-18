using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    
    [Serializable]
    public struct PieceSkin
    {
        public Vector3 RotY;
        public Mesh Mesh;
        public Material Material;
    }

    public abstract class ThingSystem <T> : PixifySytemBase where T:thing, new ()
    {
        protected virtual int InitialPieces => 30;
        protected thing.ThingPool <T> pool;

        public ThingSystem()
        {
            pool = new thing.ThingPool<T> (InitialPieces);
        }

        public sealed override void Execute()
        { 
            pool.MainForEach ();
        }
    }

    public abstract class thing : node
    {
        int version;
        public virtual void BeginStep() {}
        public virtual void Stop () { }
        public abstract bool Main ();

        public sealed class ThingPool<T> where T : thing, new()
        {
            int version;
            Queue<T> queue;
            public Dictionary<int, T> indexedPieces;
            List <T> pieces;

            public ThingPool(int baseNumber)
            {
                queue = new Queue<T>();
                indexedPieces = new Dictionary<int, T>();
                pieces = new List<T> ();

                for (int i = 0; i < baseNumber; i++)
                {
                    var p = new T();
                    p.Create();
                    queue.Enqueue(p);
                }
            }

            public void MainForEach ()
            {
                for (int i = pieces.Count - 1; i >= 0; i--)
                {
                    if (pieces [i].Main ())
                    ReturnPiece (pieces [i].version);
                }
            }

            public T NextPiece ()
            {
                CheckCapacity ();
                return queue.Peek ();
            }

            public int GetPiece()
            {
                CheckCapacity ();

                version++;

                T piece = queue.Dequeue();
                indexedPieces.Add(version, piece);
                pieces.Add (piece);
                piece.version = version;
                piece.BeginStep();

                return version;
            }

            void CheckCapacity ()
            {
                if (queue.Count == 0)
                {
                    T piece0 = new T();
                    piece0.Create();
                    queue.Enqueue(piece0);
                }
            }

            public void ReturnPiece(int version)
            {
                indexedPieces[version].Stop();
                queue.Enqueue(indexedPieces[version]);
                pieces.Remove (indexedPieces[version]);
                indexedPieces.Remove(version);
            }
        }
    }

    public abstract class ThingPointer <T> : module where T : thingptr <T>
    {
        static ThingPointer<T> o;
        protected Dictionary<int, T> ptr = new Dictionary<int, T> ();

        public ThingPointer ()
        {
            o = this;
        }

        public static void Register ( int id, T thing )
        {
            if ( o!=null )
            o.ptr.Add ( id, thing );
        }
    }

    public abstract class thingptr <T> : thing where T : thingptr<T>
    {
        public void Create ( int instanceID )
        {
            ThingPointer <T>.Register ( instanceID, (T) this );
        }
    }

}
