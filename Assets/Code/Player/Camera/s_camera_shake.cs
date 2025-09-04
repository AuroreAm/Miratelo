using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_camera_shake : dat
    {
        static s_camera_shake o;
        Animator Ani;

        protected override void OnStructured()
        {
            o = this;
            Ani = s_camera.Cam.GetComponent <Animator> ();
        }

        public static void Shake ( Vector3 pos )
        {
            o.Ani.Play ( "shake0" );
        }
    }
}