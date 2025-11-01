using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [paper ( typeof (Selector) ) ]
    [path ("decorator")]
    public class selector : action_decorator, mica <select>
    { 
        action ptr;

        Dictionary <term, action> branches;

        public selector ( List <term> names, List <action> actions ) {
            core.set_childs ( actions.ToArray () );
            for (int i = 0; i < names.Count; i++)
            {
                branches.Add ( names [i], actions [i] );
            }
        }

        protected override void _start() {
            ptr = o[0];
            ptr.tick (this);
        }

        protected override void _step() {
            ptr.tick (this);
        }

        protected override void _abort() {
            if ( ptr.on )
            ptr.abort (this);
        }

        public override void _star_stop(star s) {
            if (!on)
            return;

            stop ();
        }

        public bool _radiate(select gleam) {
            if (!on)
            return false;

            if ( !branches.ContainsKey (gleam.name) )
            return false;

            ptr.abort (this);
            ptr = branches [ gleam.name ];

            ptr.tick (this);

            return true;
        }
    }

    public struct select {
        public term name;

        public select (term _name ) {
            name = _name;
        }
    }

    public class Selector : CustomActionPaper<selector> {
        protected override selector Instance() {
            List <action> childs = new List<action> ();
            List <term> names = new List<term> ();

            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).TryGetComponent<ActionPaper>(out var c))
                    childs.Add(c.Write(_host));
                    names.Add (c.formal_term ());
            }

            return new selector ( names, childs );
        }
    }
}
