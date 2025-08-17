using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class ReactionPaper : ThoughtAuthor <guard>
    {
        [SerializeField]
        PixPaper <reaction> [] paper;

        List <reaction_guard> subscribers = new List <reaction_guard> ();

        public FlowPaper FlowAnchor;
        public ReactionPaper GuardAnchor;
        
        reaction GetReaction (int i)
        {
            reaction p = paper [i].Write ();

            if (FlowAnchor && p is reaction_flow rf)
            FlowAnchor.Subscribe (rf);
            if (GuardAnchor && p is reaction_guard rg)
            GuardAnchor.Subscribe (rg);

            return p;
        }

        protected override guard Get(block b)
        {
            reaction [] reactions = new reaction [ paper.Length ];
            for (int i = 0; i < paper.Length; i++)
            {
                reactions [i] = GetReaction (i);
                b.IntegratePix ( reactions [i] );
            }

            var t = new guard ( reactions,  transform.GetChild (0).GetComponent <ThoughtAuthor>().Write (b) );

            b.IntegratePix ( t );

            
            foreach ( var s in subscribers )
            typeof (reaction_guard).GetProperty ( "anchor", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ).SetValue ( s, t );
            subscribers = null;

            return t;
        }

        public void Subscribe ( reaction_guard r )
        {
            subscribers.Add ( r );
        }
    }
}