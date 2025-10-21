using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class CameraWriter : Writer
    {
        public Camera Camera;

        protected override void __create () {
            new camera.ink ( Camera, transform );
        }
    }
}