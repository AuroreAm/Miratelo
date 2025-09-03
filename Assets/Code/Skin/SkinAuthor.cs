using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinAuthor : AuthorModule
    {
        public float offsetRotationY;
        public float offsetPositionY;

        public override void OnStructure ()
        {
            new s_skin.package ( gameObject, new Vector2 (offsetRotationY, offsetPositionY ) );

            var modules = GetComponents<SkinAuthorModule>();
            foreach (var a in modules)
            a.OnStructure ();
        }
    }
}
