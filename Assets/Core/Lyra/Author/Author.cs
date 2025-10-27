using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public interface creator
    {
        public void _create ();
        public void _created ( system s );
    }

    public abstract class ExtWriter : MonoBehaviour {
        protected system s { get; set; }
        public void WriteTo ( system system ) {
            s = system;
            WriteTo ();
            s = null;
        }

        protected abstract void  WriteTo ( );
        protected void add ( moon moon ) {
            s.add ( moon );
        }
    }

    public abstract class Writer : MonoBehaviour, creator {
        public system Write () {
            return new system.creator (this).create_system ();
        }

        public void _create() {
            __create ();
        }

        public void _created(system s) {
            __created (s);
            Destroy (this);
        }

        protected virtual void __create () {}
        protected virtual void __created ( system s ) {}
    }

    public abstract class WriterModule : MonoBehaviour {

        public void Create () {
            _create ();
        }

        public void Created ( system s ) {
            _created (s);
            Destroy (this);
        }

        protected virtual void _create () {}
        protected virtual void _created ( system s ) {}
    }

    public abstract class AuthorModule : MonoBehaviour {
        public virtual void _create () {}
        public virtual void _created (system s) {}
    }

    public abstract class CharacterAuthor : MonoBehaviour {
        public abstract void Spawn ();
    }
}