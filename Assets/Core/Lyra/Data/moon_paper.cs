using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [Serializable]
    public struct moon_paper <T> where T:moon
    {
        public type_paper type;
        public string data;

        public T write ()
        {
            if ( !type.valid () )
                throw new InvalidOperationException ( $"{type.content} typepaper has invalid content" );
                
            T p = Activator.CreateInstance ( System.Type.GetType (type.content) ) as T;
            JsonUtility.FromJsonOverwrite ( data, p );
            return p;
        }
    }
}