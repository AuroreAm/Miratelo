using System;
using UnityEngine;

namespace Lyra
{
    [Serializable]
    public struct type_paper
    {
        public string content;

        public bool valid () => !string.IsNullOrEmpty ( content ) && Type.GetType ( content ) != null;

        public type_paper ( string content )
        {
            this.content = content;
        }

        public Type write ()
        {
            if ( !string.IsNullOrEmpty ( content ) )
            {
                Type t = Type.GetType (content);
                if ( t == null )
                throw new System.InvalidOperationException ( "type paper has invalid content" );
                return t;
            }
            throw new System.InvalidOperationException ( "type paper has invalid content" );
        }
    }
}
