using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class ElementWriter : MonoBehaviour
    {
        public enum Type { skin, metal }
        public Type type;

        public element GetElement ()
        {
            Destroy (this);
            switch (type)
            {
                case Type.skin:
                    return new e_skin ();
                case Type.metal:
                    return new e_metal ();
                default:
                    return null;
            }
        }
    }
}
