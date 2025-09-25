using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public sealed class ScriptAuthor : AuthorModule
    {
        
        public sealed override void _create () {}

        public override void _created (system system)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if ( transform.GetChild (i).TryGetComponent <IndexPaper> ( out var c ) )
                system.get <script> ().add_index ( c.write () );
            }
        }
    }
}