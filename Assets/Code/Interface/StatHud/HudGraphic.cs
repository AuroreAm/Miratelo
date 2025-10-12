using System;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    [RequireComponent(typeof(CanvasRenderer))]
    public abstract class HudGraphic : MaskableGraphic {
        public Texture2D tex;
        public sealed override Texture mainTexture => tex;

        public Action <VertexHelper> _vh;

        protected sealed override void OnPopulateMesh(VertexHelper vh) {
            vh.Clear ();
            _vh?.Invoke ( vh );
        }
    }
}