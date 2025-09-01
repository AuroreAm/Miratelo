using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_camera_shake : pix
    {
        Animator Ani;

        static s_camera_shake o;
        public override void Create()
        {
            o = this;
            Ani = s_camera.cam.GetComponent <Animator> ();
        }

        public static void Shake ( Vector3 pos )
        {
            o.Ani.Play ( "shake0" );
        }
    }
}