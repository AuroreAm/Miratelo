using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public abstract class thought : pix
    {
        thought host;
        public bool on { private set; get; }

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

        public abstract class chain : thought
        {

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

            public void Free ( thought _host )
            {
                if ( on && host == _host )
                {
                    on = false;
                    host = null;
                    OnFree ();
                }
                else
                    throw new InvalidOperationException("this thought is not aquired");
            }

            protected virtual void OnFree ()
            {
            }
        }

        public abstract class package : chain
        {
            private package () {}

            public abstract class o <T> : package where T: final, new ()
            {
                protected T main;

                public sealed override void Create()
                {
                    main = b.RequirePix <T> ();
                }

                protected sealed override void OnAquire()
                {
                    BeforeStart ();
                    main.Aquire (this);
                }

                protected virtual void BeforeStart ()
                {}

                protected sealed override void OnFree()
                {
                    main.on = false;
                    main.host = null;
                }
            }
        }

        public abstract class final : chain
        {
            public void Finish ()
            {
                if ( !on )
                throw new InvalidOperationException("this thought is already finished");
                
                on = false;
                var _host = host;
                host = null;
                _host.GuestSelfFree ( this );
            }
        }
    }

    public abstract class reaction_flow : reaction
    {
        /// <summary>
        /// invalid on Create
        /// </summary>
        protected flow anchor { private set; get; }
    }

    public abstract class reaction_guard : reaction
    {
        /// <summary>
        /// invalid on Create
        /// </summary>
        protected guard anchor { private set; get; }
    }
}