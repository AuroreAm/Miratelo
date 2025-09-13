using System;
using System.Collections.Generic;
using UnityEngine;

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
            phoenix.core.link ( linked );
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
            Dev.Break ( $"abort of stopped {this.GetType()}" );
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
            Dev.Break ( $"double stop of {this.GetType()}" );
        }

        public void tick ( core_kind _core)
        {
            if ( on == false )
                core = _core;
            

            if ( _core != core )
            Dev.Break ( $"{this.GetType()} can't be ticked by corekind {_core} as this is not the original ticker" );

            tick ( );
        }

        void notify_core_on_stopping ()
        {
            var c = core;
            core = null;
            c._star_stop (this);
        }

        public void stop (  core_kind handler )
        {
            if ( handler == core )
            abort ( );
            else
            Dev.Break ( $"{this.GetType()} can't be stopped by the corekind {handler} as this is not the original ticker" );
        }

        public abstract class main : star
        {}

        public abstract class neutron : star
        {
            protected new void stop ()
            {
                base.stop (  );
            }
        }
    }

    public interface core_kind
    {
        /// <summary> called when a ticked star stops, careful when the ticker is another star, it will always call </summary>
        public void _star_stop ( star s );
    }
}

/*using System;
using System.Collections.Generic;
using UnityEngine;

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
            phoenix.core.link ( this, linked );
        }

        protected void unlink ( star.main linked )
        {
            web.Remove ( linked );
            linked.stop (
            #if UNITY_EDITOR
            new StarEvent ( this, "manual unlink", Environment.StackTrace )
            #endif
            );
        }

        void clear_web ()
        {
            foreach ( star link in web )
            link.stop (
            #if UNITY_EDITOR
            new StarEvent ( this, "unlink", Environment.StackTrace )
            #endif
            );
            web.Clear ();
        }

        private void tick (
            #if UNITY_EDITOR
            StarEvent EVENT
            #endif
        )
        {
            if (!on)
            {
                on = true;

                enter_my_system_field ();
                #if UNITY_EDITOR
                AddJournalEvent ( EVENT );
                #endif
                _start ();
                exit_my_system_field ();

                return;
            }
            if (on)
            {
                enter_my_system_field ();
                Current = EVENT;
                _step ();
                exit_my_system_field ();
            }
        }

        private void abort (
        #if UNITY_EDITOR
        StarEvent EVENT
        #endif
        )
        {
            #if UNITY_EDITOR
            AddJournalEvent ( EVENT );
            #endif

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
            Dev.Break ( $"abort of stopped {this.GetType()}" );
        }

        private void stop (
        #if UNITY_EDITOR
        StarEvent EVENT
        #endif
        )
        {
            #if UNITY_EDITOR
            AddJournalEvent ( EVENT );
            #endif

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
            Dev.Break ( $"double stop of {this.GetType()}" );
        }

        public void tick ( core_kind _core
        #if UNITY_EDITOR
        , StarEvent EVENT
        #endif
        )
        {
            if ( on == false )
                core = _core;
            

            if ( _core != core )
            Dev.Break ( $"{this.GetType()} can't be ticked by corekind {_core} as this is not the original ticker" );

            tick (
                #if UNITY_EDITOR
                EVENT
                #endif
             );
        }

        void notify_core_on_stopping ()
        {
            var c = core;
            core = null;
            c._star_stop (this);
        }

        public void stop (  core_kind handler
        #if UNITY_EDITOR
        , StarEvent EVENT
        #endif
         )
        {
            if ( handler == core )
            abort ( EVENT );
            else
            Dev.Break ( $"{this.GetType()} can't be stopped by the corekind {handler} as this is not the original ticker" );
        }

        public abstract class main : star
        {}

        public abstract class neutron : star
        {
            protected void stop ()
            {
                base.stop ( 
                    #if UNITY_EDITOR
                    new StarEvent ( this, "selfstop", Environment.StackTrace )
                    #endif
                 );
            }
        }

        #if UNITY_EDITOR
        void AddJournalEvent ( StarEvent EVENT )
        {
            EVENT.Original = this;
            Journal.Add (EVENT);
            GlobalJournal.Add (EVENT);
        }
        List <StarEvent> Journal = new List<StarEvent> ();
        static List <StarEvent> GlobalJournal = new List<StarEvent> ();
        StarEvent Current;
        #endif
    }

    #if UNITY_EDITOR
    public class StarEvent
    {
        public float Time {get;}
        public moon Caller;
        public string Description;
        public string StackTrace {get;}
        public moon Original;

        public StarEvent ( moon caller, string description, string stacktrace )
        {
            Caller = caller;
            Description = description;
            StackTrace = stacktrace;
            Time = UnityEngine.Time.time;
        }
    }
    #endif

    public interface core_kind
    {
        /// <summary> called when a ticked star stops, careful when the ticker is another star, it will always call </summary>
        public void _star_stop ( star s );
    }
}*/