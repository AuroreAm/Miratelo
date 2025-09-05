using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class Creator : MonoBehaviour, creator
    {
        public void Start ()
        {
            new system.creator (this).create_system ();
            Destroy ( this );
        }

        public abstract void _creation ();
    }

    public abstract class CreatorModule : MonoBehaviour
    {
        public abstract void _creation ();

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