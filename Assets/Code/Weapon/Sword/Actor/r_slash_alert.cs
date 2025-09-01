using UnityEngine;
using Lyra.Spirit;
using Lyra;
using System.Collections.Generic;

namespace Triheroes.Code
{
    public class r_slash_alert : reflexion, IElementListener<incomming_slash>
    {
        List <incomming_slash> IncommingSlashes = new List<incomming_slash> ();

        public bool Alert => IncommingSlashes.Count > 0;
        public incomming_slash IncommingSlash;

        public void OnMessage ( incomming_slash context )
        {
            IncommingSlashes.Add ( context );
        }

        protected override void Reflex()
        {
            IncommingSlash.Duration = Mathf.Infinity;

            for (int i = IncommingSlashes.Count - 1; i >= 0; i--)
            {
                if ( IncommingSlashes [i].Duration <= 0 )
                {
                    IncommingSlashes.RemoveAt (i);
                    continue;
                }

                IncommingSlashes [i] = new incomming_slash ( IncommingSlashes [i].Sender, IncommingSlashes [i].Slash, IncommingSlashes[i].Duration - Time.deltaTime );

                if ( IncommingSlashes [i].Duration < IncommingSlash.Duration )
                IncommingSlash = IncommingSlashes [i];
            }
        }
    }
}