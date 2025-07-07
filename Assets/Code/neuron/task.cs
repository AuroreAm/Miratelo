using UnityEngine;
using Pixify;
using System.Collections.Generic;

namespace Triheroes.Code
{
    public enum TaskStatus { pending, running, failed, success }

    public abstract class task : action
    {
        public abstract SuperKey TaskID { get; }
        private Dictionary <int, object> Preconditions = new Dictionary<int, object> ();

        protected abstract void GetPreconditions ( ref Dictionary <int,object> Preconditions );

        public Dictionary <int,object> GetPreconditions ()
        {
            Preconditions.Clear ();
            GetPreconditions ( ref Preconditions );
            return Preconditions;
        }

        [Depend]
        protected mc_motor mm;

        protected TaskStatus status;

        protected sealed override void BeginStep()
        {
            status = TaskStatus.pending;
            BeginTask();
        }

        protected sealed override bool Step()
        {
            Task ();
            switch (status)
            {
                case TaskStatus.pending:
                return false;
                case TaskStatus.running:
                return false;
                case TaskStatus.failed:
                return true;
                case TaskStatus.success:
                return true;
                default: return false;
            }
        }

        protected virtual void BeginTask ()
        {}

        protected virtual void Task ()
        {}
    }
    
    public class motor_main_task : task
    {
        motor main;
        ITaskable taskable;

        static motor _main;

        public override SuperKey TaskID => taskable.TaskID;

        public static motor_main_task New ( ICatomFactory factory, motor main )
        {
            _main = main;
            return New <motor_main_task> ( factory );
        }
        
        public motor_main_task ()
        {
            main = _main;
            _main = null;
            taskable = main as ITaskable;

            if (taskable == null)
            Debug.LogError (" motor main task instanced without motor, or motor invalid, use factory instead");
        }

        protected override void Task()
        {
            if ( status == TaskStatus.pending )
            {
                if (mm.SetState (main))
                status = TaskStatus.running;
                else
                status = TaskStatus.failed;
            }

            if ( status == TaskStatus.running && mm.state != main)
            status = TaskStatus.success;
        }

        protected override void GetPreconditions( ref Dictionary<int, object> Preconditions)
        {
            taskable.GetPreconditions ( Preconditions );
        }
    }

    public interface ITaskable
    {
        SuperKey TaskID { get; }
        public void GetPreconditions ( Dictionary <int,object> Preconditions );
    }
}