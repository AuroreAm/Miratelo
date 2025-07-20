using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class RolePlay : MonoBehaviour
    {
        public (term,thought.chain) [] GetThoughtConcepts ( block b )
        {
            List < (term, thought.chain) > a = new List<(term, thought.chain)> ();

            for (int i = 0; i < transform.childCount; i++)
            a.Add ( ( new term ( transform.GetChild (i).gameObject.name ), GetThought ( transform.GetChild (i).GetComponent <ThoughtAuthor> () , b ) ) );

            return a.ToArray ();
        }

        thought.chain GetThought ( ThoughtAuthor T, block b )
        {
            return T.Write ( b );
        }

        public void Start ()
        {
            Destroy (gameObject);
        }
    }

    public abstract class ThoughtAuthor : MonoBehaviour
    {
        public abstract thought.chain Write ( block b );
    }

    public abstract class ThoughtAuthor <T> : ThoughtAuthor where T : thought.chain
    {
        T result;

        public sealed override thought.chain Write ( block b ) => WriteGeneric ( b );

        T WriteGeneric ( block b )
        {
            if (result != null)
                return result;

            result = Get ( b );
            return result;
        }

        protected abstract T Get ( block b );
    }
}
