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
            Vector3 look = camera.o.unity_camera.transform.position - arrow.position;
            Quaternion billboardRot = Quaternion.LookRotation(look);

            Graphics.DrawMesh(skin.mesh, arrow.position, billboardRot.applied_after (skin.roty), skin.material, 0);
        }

    }
}
