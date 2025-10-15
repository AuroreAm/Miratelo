using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public abstract class ActionPaperBase : MonoBehaviour {
        public abstract action write( moon host );
    }

    public class ActionPaper : ActionPaperBase {
        public moon_paper<action> Paper;

        public override action write ( moon host ) {
            var a = Paper.write (host);
            if (a is decorator_kind d)
                PopulateDecorator(d, host);
            return a;
        }

        public void PopulateDecorator(decorator_kind d, moon host) {
            List<action> Childs = new List<action>();

            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).TryGetComponent<ActionPaper>(out var c))
                    Childs.Add(c.write(host));
            }

            d.set(Childs.ToArray());
        }

#if UNITY_EDITOR
        public bool IsDecoratorKind() {
            return
            Paper.type.valid() &&
            typeof(decorator_kind).IsAssignableFrom(Paper.type.write());
        }
#endif
    }
}