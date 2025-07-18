using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class PixPool <T> : pix where T : pix, new()
    {
        Queue <T> queue = new Queue<T>();
        List <T> pending = new List<T> ();
        int currentFrame;

        Action<T> BeforeGet;
        Action<T> BeforeReturn;
        Action <T> AfterInstance;

        public PixPool (Action<T> BeforeGet, Action<T> BeforeReturn, Action<T> AfterInstance)
        {
            this.BeforeGet = BeforeGet;
            this.BeforeReturn = BeforeReturn;
            this.AfterInstance = AfterInstance;
        }

        public T RentPix ()
        {
            CheckCapacity ();
            var a = queue.Dequeue ();
            BeforeGet?.Invoke (a);
            return a;
        }

        void CheckCapacity()
        {
            if (queue.Count == 0)
            {
                var a = new T ();
                AfterInstance?.Invoke (a);
                queue.Enqueue(a);
            }
        }

        public void Returnpix (T a) 
        {
            BeforeReturn?.Invoke (a);

            pending.Add (a);
            currentFrame = Time.frameCount;

            // to make sure the pix is not used again in the same frame, they are moved to the pending list first then reused on a later frame
            if ( Time.frameCount != currentFrame && pending.Count > 0 )
            {
                foreach (var p in pending)
                    queue.Enqueue (p);
                pending.Clear();
            }
        }

    }
}
