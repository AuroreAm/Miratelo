using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class Author : MonoBehaviour, IAuthor
    {
        public void Start ()
        {
            new shard.constelation.write (this).constelation ();
            Destroy ( this );
        }

        public abstract void writings ();
    }

    public abstract class AuthorModule : MonoBehaviour
    {
        public abstract void OnStructure ();

        public void Start ()
        {
            Destroy (this);
        }
    }

}