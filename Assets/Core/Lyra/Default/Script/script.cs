using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public class script : moon {
        Dictionary<term, index> library = new Dictionary<term, index>();

        public void add_index(index ind) {
            system.add(ind);
            library.Add(ind.name, ind);
        }

        public index this[term id] => library[id];
    }


    [path("")]
    public sealed class index : decorator {
        public term name { private set; get; }
        action[] child => o;
        public bool repeat;
        public bool absolute;
        public bool reset;

        public index(string _name) {
            name = new term(_name);
        }

        protected override void _start() {
            Debug.LogError("index is not supposed to start");
        }

        public sequence instance_sequence () {
            var s = sequence.new_sequence (child);
            s.repeat = repeat;
            s.reset = reset;
            return s;
        }

        public tasks instance_tasks () {
            var t = tasks.new_tasks (child);
            t.repeat = repeat;
            t.reset = reset;
            return t;
        }

        public override void _star_stop(star s) {
        }
    }
}