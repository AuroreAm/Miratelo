using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;

namespace Triheroes.Code
{
    public class hud_pos : star {

        [link]
        capsule capsule;

        public Vector2 stamina () {
            Vector3 position = capsule.cc.center + capsule.cc.transform.position + vecteur.ldir ( camera.o.tps_roty, Vector3.left * ( 0.5f + capsule.r ) );
            Vector3 ppos = camera.o.unity_camera.WorldToViewportPoint ( position );
            return new Vector2 ( ppos.x * ui.wd, ppos.y * ui.hd );
        }

    }
}