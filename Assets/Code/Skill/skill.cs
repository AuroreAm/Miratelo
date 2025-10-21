using System;
using System.Collections.Generic;
using System.Reflection;
using Lyra;

namespace Triheroes.Code {
    public sealed class skills : moon {

        Dictionary < Type, skill > library = new Dictionary<Type, skill> ();

        public void add (skill skill) {
            library.Add ( skill.get_base (skill.GetType()), skill );
        }

        public bool contains <T> () where T:skill {
            return library.ContainsKey ( typeof (T) );
        }

        public T get <T> ( ) where T : skill {
            return (T) library [typeof (T)];
        }
    }

    [path ("skill")]
    public abstract class skill : moon {
        public static Type get_base ( Type skill_type ) {
            Type current = skill_type;

            while ( current != typeof (skill) ) {
                if ( current.GetCustomAttribute < skill_baseAttribute > () != null )
                return current;
                current = current.BaseType;
            }

            return skill_type;
        }
    }

    [AttributeUsage (AttributeTargets.Class)]
    public sealed class skill_baseAttribute : Attribute {
    }
}