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
        
        task_decorator domain;

        protected override void _ready() {
            domain = task_decorator.get_domain ();
        }

        protected override bool _can_start() {
            return tasks.can_start ();
        }

        public void _radiate(system_ready gleam) {
            tasks = (tasks) script [term];
        }

        protected override void _start() {
            domain.substitute ( tasks );
        }
    }
}