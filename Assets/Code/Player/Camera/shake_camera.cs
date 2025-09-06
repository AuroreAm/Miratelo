using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class shake_camera : moon
    {
        static shake_camera o;
        Animator ani;

        protected override void _ready()
        {
            o = this;
            Camera cam = (Camera) camera.o;
            ani = cam.GetComponent <Animator> ();
        }

        public static void Shake ( Vector3 pos )
        {
            o.ani.Play ( "shake0" );
        }
    }
}