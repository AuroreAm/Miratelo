using Lyra;
using UnityEngine;

namespace Lyra
{
    public abstract class task : action { 

        task_decorator domain;

        protected override void _ready() {
            domain = task_decorator.get_domain ();
        }

        protected void fail () {
            domain.task_failed ();
            stop ();
        }

        public bool can_start () {
            if (on)
            return _can_start ();
            else
            {
                Debug.LogError (" not supposed to check if task can start if the task is still running ");
                return false;
            }
        }

        protected virtual bool _can_start () { return true; }
    }

    public enum task_status { running, failed }
}