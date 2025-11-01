using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    public abstract class ActionPaper : ExtWriter {
        
        public const string _ = "-";
        public abstract action Write( moon host );
        public term formal_term () {
            return new term ( string.Equals ( gameObject.name , _ ) ? gameObject.GetInstanceID ().ToString () : gameObject.name );
        }

        protected override void WriteTo() {
            script script = s.get <script> ();
            var a = Write ( script );
            script.add_or_change ( a, formal_term () );
        }
        
        #if UNITY_EDITOR
        public bool IsDecoratorKind () {
            return
            GetPaperType () != null &&
            typeof(decorator).IsAssignableFrom ( GetPaperType () );
        }

        public virtual Type GetPaperType () => null;
        #endif
    }

    public abstract class CustomActionPaper <T> : ActionPaper where T:action {
        public sealed override action Write(moon host) {
            var p = Instance ();
            host.system.add (p);
            return p;
        }

        protected abstract T Instance ();

        #if UNITY_EDITOR
        public override Type GetPaperType () => typeof (T);
        #endif
    }

    public sealed class ActionPaperBase : ActionPaper {
        public moon_paper<action> Paper;

        public override action Write ( moon host ) {
            var a = Paper.write (host);
            if (a is decorator d)
                PopulateDecorator(d, host);
            return a;
        }

        public void PopulateDecorator(decorator d, moon host) {
            List<action> Childs = new List<action>();

            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).TryGetComponent<ActionPaper>(out var c))
                    Childs.Add(c.Write(host));
            }

            d.contract.set_childs (Childs.ToArray());
        }

        #if UNITY_EDITOR
        public override Type GetPaperType () => Paper.have_type () ? Paper.get_type () : base.GetPaperType ();
        #endif
    }
}