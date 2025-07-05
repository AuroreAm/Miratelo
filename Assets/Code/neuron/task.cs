using Pixify;

namespace Triheroes.Code
{
    public enum TaskStatus { pending, running, failed, success }

    public abstract class task : action
    {
        [Depend]
        protected mc_motor mm;

        TaskStatus status;

        protected sealed override void BeginStep()
        {
            status = TaskStatus.pending;
            BeginTask();
        }

        protected virtual void BeginTask ()
        {}

        protected sealed override bool Step()
        {
            TaskStatus taskStatus = Task ();
            status = taskStatus;
            switch (taskStatus)
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

        protected virtual TaskStatus Task ()
        {
            return TaskStatus.pending;
        }
    }

    public class motor_task : task
    {
    }
}