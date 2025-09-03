using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class CameraAuthor : Author
    {
        public Camera Camera;

        public override void OnStructure()
        {
            new s_camera.package ( Camera, transform );
        }
    }
}