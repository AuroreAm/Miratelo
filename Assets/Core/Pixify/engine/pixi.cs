using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // DOC: base component shard
    // start / step executes on the same frame
    // start when the pixi was off before
    [Category ("")]
    public abstract class pix
    {
        static uint counter = 0;
        public uint pixId { get; private set; }
        public pix () => pixId = ++counter;

        /// <summary>
        /// called when attached on a block, or initialized by other form of pixi alliance
        /// </summary>
        public virtual void Create ()
        {}

        public term term;
        protected block b {private set; get;}
    }

    
    [AttributeUsage(AttributeTargets.Field)]
    public class ExportAttribute : Attribute { }

    public class pixi : pix
    {
        public bool on { private set; get; }

        protected virtual void Start ()
        {}

        protected virtual void Step ()
        {}
        
        protected virtual void Stop ()
        {}

        protected virtual void Abort ()
        {
            Stop ();
        }

        List <int> LinkKeys = new List<int> ();
        List <pixi> Links = new List<pixi> ();
        protected void Link ( pixi p )
        {
            if ( !Links.Contains ( p ) )
            {
                Links.Add ( p );
                LinkKeys.Add ( -1 );
            }
        }

        void StartLink ()
        {
            for (int i = 0; i < Links.Count; i++)
                LinkKeys [i] = Stage.Start ( Links [i] );
        }

        void EndLink ()
        {
            for (int i = 0; i < Links.Count; i++)
            {
                Stage.Stop ( LinkKeys [i] );
                LinkKeys [i] = -1;
            }
        }

        private void iTick ()
        {
            if (!on)
            {
                on = true;
                Start ();
                StartLink ();
                return;
            }

            if (on)
            Step ();
        }

        private void iAbort ()
        {
            if (on)
            {
                on = false;
                Abort ();
                EndLink ();
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
                Stop ();
                EndLink ();
                HandleEnd ();
            }
            else
            throw new InvalidOperationException("cannot stop node that is already stopped");
        }

        IPixiHandler handler;

        public void Tick ( IPixiHandler _handler )
        {
            if ( on == false )
            {
                handler = _handler;
            }

            if ( handler != _handler )
            throw new InvalidOperationException(" this can only handled by its handler, can't handle it again until it is stopped by the handler or stopped by itself ");

            iTick ();
        }

        void HandleEnd ()
        {
            handler.OnPixiEnd (this);
            handler = null;
        }

        public void ForceStop (  IPixiHandler _handler )
        {
            if ( handler == _handler )
            iAbort ();
            else
            throw new InvalidOperationException("wrong handle, can't stop something this handle don't handle ");
        }

        public abstract class managed : pixi
        {}

        public abstract class self_managed : pixi
        {
            protected void SelfStop ()
            {
                iStop ();
            }
        }
    }



    public interface IPixiHandler
    {
        public void OnPixiEnd ( pixi p );
    }

    // used on a block to tell that the block need this another pixi
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class DependAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class,Inherited = false)]
    public class PixiBaseAttribute : Attribute
    {}
}