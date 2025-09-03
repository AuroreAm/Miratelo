using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class Author : MonoBehaviour, IStructureAuthor
    {
        public void Start ()
        {
            new dat.structure.Creator (this).CreateStructure ();
            Destroy ( this );
        }

        public abstract void OnStructure ();
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