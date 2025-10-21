using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lyra
{
    [Serializable]
    public struct moon_paper <T> where T:moon {
        
        [SerializeField]
        private string data;
        [SerializeField]
        private string type;

        private T paper_cache;

        public T paper {
            get { if ( paper_cache == null )
                paper_cache = write ();
             return paper_cache;
            }
        }

        public moon_paper ( string data, string type ) {
            this.data = data;
            this.type = type;
            paper_cache = null;
        }

        public moon_paper ( Type _type ) {
            paper_cache = null;
            type = "";
            data = "";
            if (_type != null && _type.IsSubclassOf(typeof(moon))) {
                type = _type.AssemblyQualifiedName;

                if (_type.GetConstructor(Type.EmptyTypes) != null)
                data = JsonUtility.ToJson ( Activator.CreateInstance (_type) );
            }
        }

        public readonly bool have_type () {
            return !string.IsNullOrEmpty(type) && Type.GetType(type) != null;
        }

        public Type get_type() {
            if ( have_type () )
                return System.Type.GetType(type);
            return null;
        }

        public bool valid () {
            return have_type () && get_type ().GetConstructor(Type.EmptyTypes) != null;
        }

        public T write () {
            if ( !valid () ) throw new InvalidOperationException($"Type: {this.type} is not valid");

            var type = get_type ();
            T instance = (T)Activator.CreateInstance (type);
            JsonUtility.FromJsonOverwrite (data, instance);
            return instance;
        }

        public T write (moon host) {
            var p = write ();
            host.system.add ( p );
            return p;
        }

        public void save () {
            data = JsonUtility.ToJson ( paper );
        }

        public void stream_to_SerializedProperty ( SerializedProperty sp ) {
            save ();
            sp.FindPropertyRelative (nameof (data)).stringValue = data;
            sp.FindPropertyRelative (nameof (type)).stringValue = type;
            sp.serializedObject.ApplyModifiedProperties ();
        }

    }
}