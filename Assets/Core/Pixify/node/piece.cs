using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class piece : node
    {
        public virtual void BeginStep () {}
        public virtual void Stop () {}
    }


    public abstract class PieceSystem <T> : PixifySytemBase where T : piece, new ()
    {
        Queue <T> queue;
        protected List <T> pieces;

        public PieceSystem ()
        {
            queue = new Queue<T> ();
            pieces = new List<T> ();
        }

        protected T PeekPiece ()
        {
            if (queue.Count == 0)
                {
                    T piece0 = new T ();
                    piece0.Create ();
                    queue.Enqueue (piece0);
                }
            return queue.Peek ();
        }

        protected T GetPiece ()
        {
            if (queue.Count == 0)
            {
                T piece0 = new T ();
                piece0.Create ();
                queue.Enqueue (piece0);
            }

            T piece = queue.Dequeue ();
            pieces.Add (piece);
            piece.BeginStep ();
            return piece;
        }

        protected void ReturnPiece (T piece)
        {
            piece.Stop ();
            pieces.Remove (piece);
            queue.Enqueue (piece);
        }
    }

    public abstract class PieceDataSystem <T> where T : struct
    {
        public T[] data;
        public int effectiveSize { private set; get; }

        public PieceDataSystem (int ensuredSize)
        {
            data = new T[ensuredSize];
            effectiveSize = 0;
        }

        public void SetEffectiveSize (int size)
        {
            effectiveSize = size;
            if (effectiveSize > data.Length)
                {
                Array.Resize (ref data, effectiveSize);
                }
        }
    }
}