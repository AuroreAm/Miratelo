using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public sealed class ScriptAuthor : ExtWriter
    {
        protected sealed override void WriteTo ()
        {
            script s = this.s.get <script> ();
            for (int i = 0; i < transform.childCount; i++)
            {
                if ( transform.GetChild (i).TryGetComponent <ActionPaper> ( out var c ) )
                s.add_or_change_index ( c.Write (s), new term (c.gameObject.name) );
            }
        }
    }
}