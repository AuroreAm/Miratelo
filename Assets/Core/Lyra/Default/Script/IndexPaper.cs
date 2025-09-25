using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public sealed class IndexPaper : MonoBehaviour {
        public bool Repeat = false;
        public bool Reset = true;
        public bool Absolute = false;

        public index write() {
            var a = new index(gameObject.name);
            a.repeat = Repeat;
            a.reset = Reset;
            a.absolute = Absolute;

            action[] Childs = new action[transform.childCount];
            for (int i = 0; i < Childs.Length; i++)
                Childs[i] = transform.GetChild(i).GetComponent<ActionPaper>().write();
            a.set(Childs);

            return a;
        }
    }
}
