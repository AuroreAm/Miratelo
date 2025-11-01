using UnityEngine;

namespace Lyra
{
    public abstract class task : action {  
        protected void fail () {
            look_for_task_decorator_parent ().task_failed ();
            if (on)
            stop ();
        }

        public bool can_start () {
            if (!on)
            return _can_start ();
            else {
                Debug.LogError (" not supposed to check if task can start if the task is still running ");
                return false;
            }
        }

        protected virtual bool _can_start () { return true; }

        protected task_decorator look_for_task_decorator_parent () {
            task_decorator result = null;
            for (int i = ancestors.Count - 1; i >= 0; i--)
            {
                if (ancestors[i] is task_decorator a)
                {
                    result = a;
                    break;
                }
            }
            return result;
        }
    }

    public enum task_status { running, failed }
}