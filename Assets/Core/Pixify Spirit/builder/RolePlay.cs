using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class RolePlay : MonoBehaviour
    {

        public (term,thought) [] GetThoughtConcepts ( block b )
        {
            List < (term, thought) > a = new List<(term, thought)> ();

            for (int i = 0; i < transform.childCount; i++)
            a.Add ( ( new term ( transform.GetChild (i).gameObject.name ), GetThought ( transform.GetChild (i).GetComponent <ThoughtAuthor> () , b ) ) );

            return a.ToArray ();
        }

        thought GetThought ( ThoughtAuthor T, block b )
        {
            if ( T is ThoughtPaper thoughtFinalPaper )
            {
                var t = thoughtFinalPaper.paper.Write ();
                b.IntegratePix ( t );
                return t;
            }

            if ( T is ReflexionPaper reflexionPaper )
            {
                reflexion [] reflexions = new reflexion [ reflexionPaper.paper.Length ];
                for (int i = 0; i < reflexionPaper.paper.Length; i++)
                {
                    reflexions [i] = reflexionPaper.paper[i].Write ();
                    b.IntegratePix ( reflexions [i] );
                }

                var t = new guard ( reflexions, GetThought ( T.transform.GetChild (0).GetComponent <ThoughtAuthor>(), b ) );

                b.IntegratePix ( t );

                return t;
            }
            return null;
        }

        public void Start ()
        {
            Destroy (this.gameObject);
        }
    }

    public abstract class ThoughtAuthor : MonoBehaviour
    {}
}
