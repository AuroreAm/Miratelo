using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra {
    [star(-1)] 
    public abstract class action : star.neutron {
        protected List<action> ancestors { private set; get; } = new List<action>();

        /// <summary> make parent of this action registering it in the ancestors list </summary>
        public void descend(action parent) {
            ancestors.Clear();
            ancestors.AddRange(parent.ancestors);
            ancestors.Add(parent);
            _descend();
        }

        protected virtual void _descend() { }

        public static void start(action Script) {
            if (Script != null) {
                phoenix.core.start_action(Script);
            }
            else
                Debug.LogWarning("script is null");
        }

        public decorator look_for_decorator_parent () {
            decorator result = null;
            for (int i = ancestors.Count - 1; i >= 0; i--)
            {
                if (ancestors[i] is decorator a)
                {
                    result = a;
                    break;
                }
            }
            return result;
        }

        public sealed class root : action {
        }
    }

    [ AttributeUsage ( AttributeTargets.Class, Inherited = true ) ]
    public class paperAttribute : Attribute {
        public Type PaperType;
        public paperAttribute ( Type t ) {
            PaperType = t;
        }
    }

    [path("default")]
    public class log : action {
        [export]
        public string text;

        protected override void _step() {
            Debug.Log(text);
            stop();
        }
    }

    [path("default")]
    public class wait : action {
        [export]
        public float time;
        float t;

        protected override void _start() {
            t = time;
        }

        protected override void _step() {
            t -= Time.deltaTime;
            if (t <= 0)
                stop();
        }
    }
}