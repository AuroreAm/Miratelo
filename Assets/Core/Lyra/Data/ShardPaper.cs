using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [Serializable]
    public struct ShardPaper <T> where T:shard
    {
        public TypePaper Type;
        public string Data;

        public T radiate ()
        {
            if ( !Type.IsValid () )
                throw new InvalidOperationException ( "type paper has invalid content" );
                
            T p = Activator.CreateInstance ( System.Type.GetType (Type.Content) ) as T;
            JsonUtility.FromJsonOverwrite ( Data, p );
            return p;
        }
    }
}