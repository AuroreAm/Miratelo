using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class UnitAuthor : ScriptableObject, IUnitAuthor
    {
        public Unit Instance ()
        {
            Unit u = new Unit ();
            OnInstance (u);
            u.Create ();
            return u;
        }

        protected abstract void OnInstance ( Unit newUnit );
    }

    public interface IUnitAuthor
    {
        public Unit Instance ();
    }
}
