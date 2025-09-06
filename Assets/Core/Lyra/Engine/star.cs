using System;
using System.Collections.Generic;

namespace Lyra
{
    public class star : moon
    {
        public bool on { private set; get; }
        private core_kind core;

        private List <star.main> web = new List<star.main> ();

        protected virtual void _start ()
        {}

        protected virtual void _step ()
        {}
        
        protected virtual void _stop ()
        {}

        protected virtual void _abort ()
        {
            _stop ();
        }

        protected void link ( star.main linked )
        {
            web.Add ( linked );
            phoenix.core.start ( linked );
        }

        protected void unlink ( star.main linked )
        {
            web.Remove ( linked );
            linked.stop ();
        }

        void clear_web ()
        {
            foreach ( star link in web )
            link.stop ();
            web.Clear ();
        }

        private void tick ()
        {
            if (!on)
            {
                on = true;

                enter_my_system_field ();
                _start ();
                exit_my_system_field ();

                return;
            }
            if (on)
            {
                enter_my_system_field ();
                _step ();
                exit_my_system_field ();
            }
        }

        private void abort ()
        {
            if (on)
            {
                on = false;

                enter_my_system_field ();
                _abort ();
                clear_web ();
                exit_my_system_field ();
                
                notify_core_on_stopping ();
            }
            else
            throw new InvalidOperationException("cannot abort node that is already stopped");
        }

        private void stop ()
        {
            if (on)
            {
                on = false;

                enter_my_system_field ();
                _stop ();
                clear_web ();
                exit_my_system_field ();

                notify_core_on_stopping ();
            }
            else
            throw new InvalidOperationException("cannot stop node that is already stopped");
        }

        public void tick ( core_kind _core )
        {
            if ( on == false )
                core = _core;

            if ( _core != core )
            throw new InvalidOperationException(" this can only handled by its handler, can't handle it again until it is stopped by the handler or stopped by itself ");

            tick ();
        }

        void notify_core_on_stopping ()
        {
            core._star_stop (this);
            core = null;
        }

        public void stop (  core_kind handler )
        {
            if ( handler == core )
            abort ();
            else
            throw new InvalidOperationException("wrong handle, can't stop something this handle don't handle ");
        }

        public abstract class main : star
        {}

        public abstract class neutron : star
        {
            protected new void stop ()
            {
                base.stop ();
            }
        }
    }

    public interface core_kind
    {
        public void _star_stop ( star s );
    }
}