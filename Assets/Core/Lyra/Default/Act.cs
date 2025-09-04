using UnityEngine;

namespace Lyra
{
    [note(-1)]
    public class act : aria.act
    {
    }

    public static class Act
    {
        public static void Start (act script)
        {
            if (script != null)
            {
                phoenix.core.start ( script );
            }
            else
            Debug.LogWarning ("script is null");
        }
    }

    [verse("debug")]
    public class log : act
    {
        [lyric]
        public string Text;

        protected override void alive()
        {
            Debug.Log (Text);
            sleep ();
        }
    }
}