using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_camera_shake : pix
    {
        [Depend]
        s_camera sc;
        Animator Ani;

        static s_camera_shake o;
        public override void Create()
        {
            o = this;
            Ani = sc.Cam.GetComponent <Animator> ();
        }

        public static void Shake ( Vector3 pos )
        {
            o.Ani.Play ( "shake0" );
        }
    }
}