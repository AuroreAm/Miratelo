using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;
using System;

namespace Triheroes.Code
{
    public class CameraWriter : Writer
    {
        public override void OnWriteBlock()
        {
            new s_camera.package ( transform, transform.GetChild (0).GetComponent <Camera> () );
        }

        public override void RequiredPix( in List <Type> a )
        {
            a.A <s_camera> ();
            a.A < s_camera_shake> ();
        }
    }
}
