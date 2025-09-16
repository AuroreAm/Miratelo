using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class Author : MonoBehaviour, creator
    {
        public void Awake ()
        {
            new system.creator (this).create_system ();
            Destroy ( this );
        }

        public abstract void _create ();
        public abstract void _created ( system s );
    }

    public abstract class AuthorModule : MonoBehaviour
    {
        public abstract void _create ();
        public virtual void _created (system s) {}

        public void Start ()
        {
            Destroy (this);
        }
    }

    public interface creator
    {
        public void _create ();
        public void _created ( system s );
    }

}