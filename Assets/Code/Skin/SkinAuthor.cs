using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinAuthor : ActorAuthorModule
    {
        public float OffsetRotationY;
        public float OffsetPositionY;

        public override void _creation ()
        {
            new skin.ink ( gameObject, new Vector2 (OffsetRotationY, OffsetPositionY ) );

            var modules = GetComponents<SkinAuthorModule>();
            foreach (var a in modules)
            a._creation ();
        }

        public override void _creation(system system)
        {
            var modules = GetComponents<SkinAuthorModule>();
            foreach (var a in modules)
            a._creation (system);
        }
    }
}
