using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class arrow_billboard : spectre
    {
        [link]
        arrow arrow;

        arrow.skin skin;

        public class ink : ink<arrow_billboard>
        {
            public ink(arrow.skin skin)
            {
                o.skin = skin;
            }
        }

        protected override void _step()
        {
            Graphics.DrawMesh(skin.mesh, arrow.position, camera.o.get_billboard_rotation ( arrow.position ).applied_after (skin.roty), skin.material, 0);
        }

    }
}
