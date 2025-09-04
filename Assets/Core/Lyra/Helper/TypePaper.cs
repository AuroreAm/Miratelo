using System;
using UnityEngine;

namespace Lyra
{
    [Serializable]
    public struct TypePaper
    {
        public string Content;

        public bool IsValid () => !string.IsNullOrEmpty ( Content ) && Type.GetType ( Content ) != null;

        public TypePaper ( string content )
        {
            Content = content;
        }

        public Type ExtractType ()
        {
            if ( !string.IsNullOrEmpty ( Content ) )
            {
                Type t = Type.GetType (Content);
                if ( t == null )
                throw new System.InvalidOperationException ( "type paper has invalid content" );
                return t;
            }
            throw new System.InvalidOperationException ( "type paper has invalid content" );
        }
    }
}
