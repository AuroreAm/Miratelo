using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Spirit
{
    public sealed class FlowPaper : ThoughtAuthor <flow>
    {
        List <reaction_flow> subscribers = new List <reaction_flow> ();

        protected override flow Get(block b)
        {
            thought.chain [] o = new thought.chain [ transform.childCount ];

            for (int i = 0; i < o.Length; i++)
            {
                o [i] = transform.GetChild (i).GetComponent <ThoughtAuthor> ().Write (b);
                b.IntegratePix ( o [i] );
            }

            var t = new flow ( o );
            b.IntegratePix ( t );

            foreach ( var s in subscribers )
            typeof (reaction_flow).GetProperty ( "anchor", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ).SetValue ( s, t );
            subscribers = null;

            return t;
        }

        public void Subscribe ( reaction_flow r )
        {
            subscribers.Add ( r );
        }
    }

}