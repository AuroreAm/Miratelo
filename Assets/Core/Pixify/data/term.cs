using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [System.Serializable]
    public struct term
    {
        [SerializeField]
        private int value;

        #if UNITY_EDITOR
        public string name;
        #endif

        public term(string Name)
        {
            value = Animator.StringToHash(Name);
            #if UNITY_EDITOR
            name = Name;
            #endif
        }

        public static implicit operator int(term key)
        {
            return key.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is term)
                return this.value == ((term)obj).value;
            return false;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
