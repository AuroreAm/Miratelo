using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public abstract class thought : pix
    {
        thought host;
        public bool on { private set; get; }

        public void Aquire ( thought _host )
        {
            if ( !on )
            {
                on = true;
                host = _host;
                OnAquire ();
            }
            else
                throw new InvalidOperationException("this thought is already aquired");
        }

        protected virtual void OnAquire ()
        {}

        void GuestSelfFree (thought guest)
        {
            if ( OnGuestSelfFree (guest) )
            {
                on = false;
                var _host = host;
                host = null;
                _host.GuestSelfFree ( this );
            }
        }

        /// <returns> if the thought should also self free and pass that to its host </returns>
        protected virtual bool OnGuestSelfFree ( thought guest )
        {
            return true;
        }

        public class final : thought
        {
            public void Finish ()
            {
                on = false;
                var _host = host;
                host = null;
                _host.GuestSelfFree ( this );
            }
        }
    }
}