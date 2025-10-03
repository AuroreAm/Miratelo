using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class slash_alert : controller, gold <incomming_slash>
    {
        List <incomming_slash> incomming_slashes = new List<incomming_slash> ();

        public bool alert => incomming_slashes.Count > 0;
        public float timeleft => incomming_slash.duration;
        public incomming_slash incomming_slash;

        protected override void _step()
        {
            incomming_slash.duration = Mathf.Infinity;

            for (int i = incomming_slashes.Count - 1; i >= 0; i--)
            {
                if ( incomming_slashes [i].duration <= 0 )
                {
                    incomming_slashes.RemoveAt (i);
                    continue;
                }

                incomming_slashes [i] = new incomming_slash ( incomming_slashes [i].sender, incomming_slashes [i].slash, incomming_slashes[i].duration - Time.deltaTime );

                if ( incomming_slashes [i].duration < incomming_slash.duration )
                incomming_slash = incomming_slashes [i];
            }
        }

        protected override void _stop()
        {
            incomming_slash.duration = Mathf.Infinity;
            incomming_slashes.Clear ();
        }

        public void _radiate (incomming_slash gleam)
        {
            incomming_slashes.Add ( gleam );
        }
    }
}
