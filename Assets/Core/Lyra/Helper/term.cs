using UnityEngine;

namespace Lyra
{
    [System.Serializable]
    public struct term
    {
        [SerializeField]
        private int value;

        #if UNITY_EDITOR
        public string name;
        #endif

        public term(string name)
        {
            value = Animator.StringToHash(name);

            #if UNITY_EDITOR
            this.name = name;
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
