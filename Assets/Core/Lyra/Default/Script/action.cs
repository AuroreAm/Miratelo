using System;
using UnityEngine;

namespace Lyra
{
    [star(-1)]
    public abstract class action : star.neutron
    {
        public static void start (action Script)
        {
            if (Script != null)
            {
                phoenix.core.start_action ( Script );
            }
            else
            Debug.LogWarning ("script is null");
        }
    }

    [path("default")]
    public class log : action
    {
        [export]
        public string text;

        protected override void _step()
        {
            Debug.Log (text);
            stop ();
        }
    }

    [path("default")]
    public class wait : action
    {
        [export]
        public float time;
        float t;

        protected override void _start()
        {
            t = time;
        }

        protected override void _step()
        {
            t -= Time.deltaTime;
            if (t <= 0)
                stop ();
        }
    }
}