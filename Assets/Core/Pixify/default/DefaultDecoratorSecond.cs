using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    // will do a sequence of actions, but will get feedback from actions, if success, continue, else, stop
    [NodeTint(0.2f,1,1)]
    public sealed class controlled_sequence : decorator
    {
        static Stack<controlled_sequence> ExecutingSequences = new Stack<controlled_sequence>();
        /// <summary>
        /// Current ticking sequence
        /// </summary>
        public static controlled_sequence CurrentSequence => ExecutingSequences.Count > 0 ? ExecutingSequences.Peek() : null;
        public static TaskStatusEnum CurrentStatus { set { CurrentSequence.TaskStatus = value; } }

        public enum TaskStatusEnum { Success, Failure }

        /// <summary>
        /// set this to define the status of this sequence, this will reset every increment
        /// </summary>
        public TaskStatusEnum TaskStatus;

        int ptr;

        [Export]
        public bool repeat = true;
        [Export]
        public bool reset = true;

        protected sealed override void BeginStep ()
        {
            if (reset)
            ptr = 0;
            TaskStatus = TaskStatusEnum.Success;

            ExecutingSequences.Push(this);
            o[ptr].iStart ();
            ExecutingSequences.Pop();
        }

        protected sealed override bool Step ()
        {
            ExecutingSequences.Push(this);
            if (o[ptr].on)
                o [ptr].iExecute ();
            ExecutingSequences.Pop();

            if (!o[ptr].on)
            {
                if (TaskStatus == TaskStatusEnum.Success)
                {
                    ptr++;
                    if (ptr >= o.Length)
                    {
                        ptr = 0;
                        if (!repeat)
                        return true;
                    }
                    
                    ExecutingSequences.Push(this);
                    o[ptr].iStart ();
                    ExecutingSequences.Pop();
                }
                else
                return true;
            }
            return false;
        }

        protected override void Stop()
        {
            if (o[ptr].on)
            o[ptr].iAbort ();
        }

        #if UNITY_EDITOR
        public override string GetAdditionalInfo()
        {
            return $"repeat: {repeat}, reset: {reset}";
        }
        #endif
    }
}
