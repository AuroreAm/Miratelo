using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [System.Serializable]
    public struct SuperKey
    {
        [SerializeField]
        private int value;

        #if UNITY_EDITOR
        public string name;
        #endif

        public SuperKey(string keyName)
        {
            value = Animator.StringToHash(keyName);
            #if UNITY_EDITOR
            name = keyName;
            #endif
        }

        public static implicit operator int(SuperKey key)
        {
            return key.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        //  Add equality operators
        public override bool Equals(object obj)
        {
            if (obj is SuperKey)
            {
                return this.value == ((SuperKey)obj).value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }

}
