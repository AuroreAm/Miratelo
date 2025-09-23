using Lyra;
using UnityEngine;
using Triheroes.Code.CameraShot;
using Triheroes.Code.Axeal;

namespace Triheroes.Code
{
    public class normal : tps_shot
    {
        protected override void _start()
        {
            h = cam.player.system.get<capsule>().h + .25f;
            distance = 4;
        }

        protected override void _step()
        {
            // rotate using the mouse
            // TODO: add sensitivity tweak, add inverted mouse
            roty += player.delta_mouse.x;
            rotx -= player.delta_mouse.y;
            rotx = Mathf.Clamp( rotx, -80, 80 );
        }
    }
}