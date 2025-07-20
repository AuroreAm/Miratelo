using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class ReflexionPaper : ThoughtAuthor <guard>
    {
        [SerializeField]
        PixPaper <reflexion> [] paper;

        List <reflexion_guard> subscribers = new List <reflexion_guard> ();

        public FlowPaper FlowAnchor;
        public ReflexionPaper GuardAnchor;
        
        reflexion GetReflexion (int i)
        {
            reflexion p = paper [i].Write ();

            if (FlowAnchor && p is reflexion_flow rf)
            FlowAnchor.Subscribe (rf);
            if (GuardAnchor && p is reflexion_guard rg)
            GuardAnchor.Subscribe (rg);

            return p;
        }

        protected override guard Get(block b)
        {
            reflexion [] reflexions = new reflexion [ paper.Length ];
            for (int i = 0; i < paper.Length; i++)
            {
                reflexions [i] = GetReflexion (i);
                b.IntegratePix ( reflexions [i] );
            }

            var t = new guard ( reflexions,  transform.GetChild (0).GetComponent <ThoughtAuthor>().Write (b) );

            b.IntegratePix ( t );

            
            foreach ( var s in subscribers )
            typeof (reflexion_guard).GetProperty ( "anchor", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ).SetValue ( s, t );
            subscribers = null;

            return t;
        }

        public void Subscribe ( reflexion_guard r )
        {
            subscribers.Add ( r );
        }
    }
}