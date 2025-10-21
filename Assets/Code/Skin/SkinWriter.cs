using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkinWriter : WriterModule
    {
        public float OffsetRotationY;
        public float OffsetPositionY;

        protected override void _create ()
        {
            new skin.ink ( gameObject, new Vector2 (OffsetRotationY, OffsetPositionY ) );
            new ink <graphic> ();

            var modules = GetComponents<SkinWriterModule>();
            foreach (var a in modules)
            a.Create ();
        }

        protected override void _created (system system)
        {
            var meshes = GetComponentsInChildren <Renderer> ();
            foreach (var m in meshes)
            system.get <graphic> ().renderers.Add (m.gameObject);

            var modules = GetComponents<SkinWriterModule>();
            foreach (var a in modules)
            a.Created (system);
        }
    }
}
