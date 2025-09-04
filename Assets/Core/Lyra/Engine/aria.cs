using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public class aria : shard
    {
        public bool on { private set; get; }
        private ICore handler;

        /// <summary> called the first frame after this system is on </summary>
        protected virtual void awaken ()
        {}

        /// <summary> called every frame this system is on </summary>
        protected virtual void alive ()
        {}
        
        /// <summary> called when stopped on purpose by the system or itslef </summary>
        protected virtual void asleep ()
        {}

        /// <summary> called when stopped but not supposed to be </summary>
        protected virtual void afaint () => asleep ();

        private void iTick ()
        {
            if (!on)
            {
                on = true;

                DiveInDomain ();
                awaken ();
                ExitDomain ();

                return;
            }
            if (on)
            {
                DiveInDomain ();
                alive ();
                ExitDomain ();
            }
        }

        private void iAbort ()
        {
            if (on)
            {
                on = false;

                DiveInDomain ();
                afaint ();
                ExitDomain ();
                
                TellCoreiSleep ();
            }
            else
            throw new InvalidOperationException("cannot abort shard that is already stopped");
        }

        private void iStop ()
        {
            if (on)
            {
                on = false;

                DiveInDomain ();
                asleep ();
                ExitDomain ();

                TellCoreiSleep ();
            }
            else
            throw new InvalidOperationException("cannot stop shard that is already stopped");
        }

        public void sing ( ICore _handler )
        {
            if ( on == false )
                handler = _handler;

            if ( _handler != handler )
            throw new InvalidOperationException(" this can only handled by its handler, can't handle it again until it is stopped by the handler or stopped by itself ");

            iTick ();
        }

        void TellCoreiSleep ()
        {
            handler.inhalt (this);
            handler = null;
        }

        public void halt ( ICore handler )
        {
            if ( handler == this.handler )
                iAbort();
            else
            throw new InvalidOperationException(" wrong handle, can't stop something this handle don't handle ");
        }

        public abstract class flow : aria
        {}

        public abstract class act : aria
        {
            protected void sleep ()
            {
                iStop ();
            }
        }
    }

    public interface ICore
    {
        public void inhalt ( aria s );
    }
}