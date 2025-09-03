using UnityEngine;

namespace Lyra
{
    [System.Serializable]
    public struct term
    {
        [SerializeField]
        private int _value;

        #if UNITY_EDITOR
        public string Name;
        #endif

        public term(string Name)
        {
            _value = Animator.StringToHash(Name);

            #if UNITY_EDITOR
            this.Name = Name;
            #endif
        }

        public static implicit operator int(term key)
        {
            return key._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is term)
                return this._value == ((term)obj)._value;
            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
