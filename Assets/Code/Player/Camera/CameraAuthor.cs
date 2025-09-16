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

        public override void _create ()
        {
            new camera.ink ( Camera, transform );
        }

        public override void _created(system s)
        {}
    }
}