using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;

namespace Triheroes.Code
{
    public class CameraWriter : Writer
    {
        public override void OnWriteBlock()
        {
            new s_camera.package ( transform, transform.GetChild (0).GetComponent <Camera> () );
        }

        public override Type[] RequiredPix()
        {
            return new Type [] { 
                Q <s_camera> (),
                Q < s_camera_shake> ()
            };
        }
    }
}
