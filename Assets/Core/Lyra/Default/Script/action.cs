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

    [path("debug")]
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
}