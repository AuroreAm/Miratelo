using Lyra;
using UnityEngine;

namespace Lyra
{
    [path ("script")]
    public sealed class substitute : task, ruby < system_ready > {
        [export]
        public term term;
        
        [link]
        script script;

        tasks tasks;

        protected override bool _can_start() {
            return tasks.can_start ();
        }

        public void _radiate(system_ready gleam) {
            tasks = (tasks) script [term];
        }

        protected override void _start() {
            task_decorator.domain.replace ( tasks );
        }
    }
}