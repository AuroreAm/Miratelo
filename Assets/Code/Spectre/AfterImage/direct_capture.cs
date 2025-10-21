using Lyra;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class direct_capture : moon {
        public static direct_capture o {private set; get;}
        Camera cam;
        Camera main_cam => camera.cam;

        public class ink : ink <direct_capture> {
            public ink ( Camera cam ) {
                o.cam = cam;
            }
        }

        protected override void _ready() {
            o = this;
        }

        public void capture ( Vector3 target_pos, float quad_size, RenderTexture tx ) {
            cam.targetTexture = tx;
            Vector3 to_main_camera = main_cam.transform.position - target_pos;
            cam.transform.SetPositionAndRotation(target_pos, Quaternion.LookRotation(to_main_camera.normalized, main_cam.transform.up));
            // Move camera slightly back to avoid clipping
            cam.transform.position -= cam.transform.forward * 0.5f;
            // This assumes quad_size is the world-space height you want to capture
            cam.orthographicSize = quad_size * 0.5f;

            cam.Render ();
        }
    }
}