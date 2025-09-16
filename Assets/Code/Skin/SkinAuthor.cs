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

        public override void _create ()
        {
            new skin.ink ( gameObject, new Vector2 (OffsetRotationY, OffsetPositionY ) );

            var modules = GetComponents<SkinAuthorModule>();
            foreach (var a in modules)
            a._create ();
        }

        public override void _created (system system)
        {
            var modules = GetComponents<SkinAuthorModule>();
            foreach (var a in modules)
            a._created (system);
        }
    }
}
