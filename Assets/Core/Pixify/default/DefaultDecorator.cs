using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [NodeTint(0,1,1)]
    public sealed class sequence : decorator
    {
        int ptr;

        public bool repeat = true;
        public bool reset = true;

        protected sealed override void BeginStep ()
        {
            if (reset)
            ptr = 0;
            o[ptr].iStart ();
        }

        protected sealed override bool Step ()
        {
            if (o[ptr].on)
                o [ptr].iExecute ();

            if (!o[ptr].on)
            {
                ptr++;
                if (ptr >= o.Length)
                {
                    ptr = 0;
                    if (!repeat)
                    return true;
                }
                o[ptr].iStart ();
            }
            return false;
        }

        protected override void Stop()
        {
            if (o[ptr].on)
            o[ptr].iAbort ();
        }
    }

    [NodeTint(0,1,0)]
    public sealed class parallel : decorator
    {
        /// <summary>
        /// if true, the node will stop when the first node stops
        /// </summary>
        public bool StopWhenFirstNodeStopped;

        protected override void BeginStep()
        {
            foreach (var n in o)
                n.iStart();
        }

        protected override bool Step()
        {
            bool Continue = false;

            foreach (var n in o)
            {
                if (n.on)
                {
                    Continue = true;
                    break;
                }
            }

            if (StopWhenFirstNodeStopped)
            {
                if (!o[0].on)
                    return true;
            }
            else if (Continue == false)
            {
                return true;
            }

            foreach (var n in o)
            {
                if (n.on)
                {
                    n.iExecute();
                }
            }

            return false;
        }

        protected override void Stop()
        {
            foreach (var n in o)
            {
                if (n.on)
                    n.iAbort();
            }
        }
    }

    public sealed class selector : decorator
    {
        static Stack<selector> ExecutingSelectors = new Stack<selector>();
        /// <summary>
        /// Current ticking selector
        /// </summary>
        public static selector CurrentSelector => ExecutingSelectors.Count > 0 ? ExecutingSelectors.Peek() : null;

        public bool reset = true;
        public bool fallback = false;

        Dictionary <SuperKey, action> Index;
        action DefaultRoot;
        action root;

        public override void Create()
        {
            Index = new Dictionary<SuperKey, action> ();
            foreach (var n in o)
                Index.AddOrChange (n.Tag, n);

            DefaultRoot = o[0];
            root = DefaultRoot;
        }

        protected override void BeginStep()
        {
            ExecutingSelectors.Push(this);
            if (reset)
                root = DefaultRoot;
            root.iStart();
            ExecutingSelectors.Pop();
        }

        protected override bool Step()
        {
            ExecutingSelectors.Push(this);

            if (root.on)
                root.iExecute();

            ExecutingSelectors.Pop();

            if (!root.on)
            {
                if (!fallback)
                return true;
                else
                SwitchTo (DefaultRoot.Tag);
            }

            return false;
        }

        protected override void Abort()
        {
            if (root.on)
                root.iAbort();
        }

        public void FallBack ()
        {
            SwitchTo (DefaultRoot.Tag);
        }

        public void SwitchTo (SuperKey tag)
        {
            if (Index.TryGetValue (tag, out action a))
            {
                if (root.on)
                    root.iAbort ();
                root = a;
                
                ExecutingSelectors.Push(this);
                root.iStart ();
                ExecutingSelectors.Pop();
            }
            else
            Debug.LogWarning ("tag " + tag + " not found");
        }
    }

    public sealed class delayrepeater : decorator
    {
        public bool DelayFirst = true;
        public int SpamCount = 1;
        public float DelayInterval = 1;

        int repeatCounter;
        float time;
        bool isRunning;

        protected override void BeginStep()
        {
            repeatCounter = 0;
            time = 0;
            if (!DelayFirst)
            {
                isRunning = true;
                o[0].iStart();
            }
        }

        protected override bool Step()
        {
            if (isRunning)
            {
                if (!o[0].on)
                {
                    isRunning = false;  // Stop running and start the timer
                    time = 0f;

                    repeatCounter++;
                    if (repeatCounter >= SpamCount)
                        return true;  // Stop the node if spam count is reached
                        
                    return false;
                }
                else
                {
                    o[0].iExecute();  // Continue updating the child if it's still running
                }
            }
            else
            {
                time += Time.deltaTime;
                if (time >= DelayInterval)
                {
                    isRunning = true;
                    o[0].iStart();  // Restart the child after the interval
                }
            }

            return false;  // Node is not yet finished
        }

        protected override void Abort()
        {
            if (o[0].on)
                o[0].iAbort();
        }
    }
}
