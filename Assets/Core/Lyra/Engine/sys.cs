using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public class sys : dat
    {
        public bool on { private set; get; }
        private ISysProcessor _handler;

        protected virtual void OnStart ()
        {}

        protected virtual void OnStep ()
        {}
        
        protected virtual void OnStop ()
        {}

        protected virtual void OnAbort ()
        {
            OnStop ();
        }

        private void iTick ()
        {
            if (!on)
            {
                on = true;

                BeginStructureDomain ();
                OnStart ();
                EndStructureDomain ();

                return;
            }
            if (on)
            {
                BeginStructureDomain ();
                OnStep ();
                EndStructureDomain ();
            }
        }

        private void iAbort ()
        {
            if (on)
            {
                on = false;

                BeginStructureDomain ();
                OnAbort ();
                EndStructureDomain ();
                
                HandleEnd ();
            }
            else
            throw new InvalidOperationException("cannot abort node that is already stopped");
        }

        private void iStop ()
        {
            if (on)
            {
                on = false;

                BeginStructureDomain ();
                OnStop ();
                EndStructureDomain ();

                HandleEnd ();
            }
            else
            throw new InvalidOperationException("cannot stop node that is already stopped");
        }

        public void Tick ( ISysProcessor handler )
        {
            if ( on == false )
                _handler = handler;

            if ( handler != _handler )
            throw new InvalidOperationException(" this can only handled by its handler, can't handle it again until it is stopped by the handler or stopped by itself ");

            iTick ();
        }

        void HandleEnd ()
        {
            _handler.OnSysEnd (this);
            _handler = null;
        }

        public void ForceStop (  ISysProcessor handler )
        {
            if ( handler == _handler )
            iAbort ();
            else
            throw new InvalidOperationException("wrong handle, can't stop something this handle don't handle ");
        }

        public abstract class ext : sys
        {}

        public abstract class self : sys
        {
            protected void Stop ()
            {
                iStop ();
            }
        }
    }

    public interface ISysProcessor
    {
        public void OnSysEnd ( sys s );
    }
}