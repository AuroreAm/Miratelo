namespace Lyra
{
    [path ("script")]
    public sealed class replace : task, ruby < system_written > {
        [export]
        public term term;
        
        [link]
        script script;

        tasks tasks;

        protected override bool _can_start() {
            return tasks.can_start ();
        }

        public void _radiate(system_written gleam) {
            tasks = (tasks) script [term];
        }

        protected override void _start() {
            tasks.descend (this);
            look_for_task_decorator_parent ().replace ( tasks );
        }
    }
}