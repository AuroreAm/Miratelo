using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public sealed class ScriptAuthor : AuthorModule
    {
        
        public sealed override void _create () {
            new ink <script> ();
        }

        public override void _created (system system)
        {
            script s = system.get <script> ();
            for (int i = 0; i < transform.childCount; i++)
            {
                if ( transform.GetChild (i).TryGetComponent <ActionPaper> ( out var c ) )
                s.add_index ( c.write (s), new term (c.gameObject.name) );
            }
        }
    }
}