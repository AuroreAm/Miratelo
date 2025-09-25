using Lyra;
using UnityEngine;

namespace Lyra
{
    [path ("script")]
    public sealed class substitute : task, ruby < system_ready > {
        [export]
        public term term;

        [export]
        public bool absolute;

        [link]
        script script;

        tasks index;

        protected override bool _can_start() {
            return index.can_start ();
        }

        public void _radiate(system_ready gleam) {
            index = script [term].instance_tasks ();
        }

        protected override void _start() {
            task_decorator.substitute ( index );
        }
    }
}