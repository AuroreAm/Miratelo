using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class CameraAuthor : Creator
    {
        public Camera Camera;

        public override void _creation()
        {
            new camera.ink ( Camera, transform );
        }
    }
}