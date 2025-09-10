using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class Author : MonoBehaviour, creator
    {
        public void Start ()
        {
            new system.creator (this).create_system ();
            Destroy ( this );
        }

        public abstract void _creation ();
    }

    public abstract class AuthorModule : MonoBehaviour
    {
        public abstract void _creation ();
        public virtual void _creation (system system) {}

        public void Start ()
        {
            Destroy (this);
        }
    }

    public interface creator
    {
        public void _creation ();
    }

}