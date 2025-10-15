using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public class script : moon {
        Dictionary<term, action> library = new Dictionary<term, action>();

        public void add_index(action ind, term name) {
            ind.descend( new action.root () );
            library.Add(name, ind);
        }

        public action this[term id] => library[id];


        #if UNITY_EDITOR
        [link]
        script_indexor sci;

        public class script_indexor : public_moon <script_indexor> {
            [link]
            internal script script;
            [link]
            character c;

            protected override void _ready() {
                register ( c.gameobject.GetInstanceID () );
            }
        }

        [superstar]
        public class crypt : index <script_indexor> {
            static crypt o;

            protected override void _ready() {
                o = this;
            }

            public static Dictionary <term, action> get (int id) {
                return o.ptr[id].script.library;
            }
        }
        #endif
    }
}