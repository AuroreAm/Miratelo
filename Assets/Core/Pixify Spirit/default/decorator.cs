using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    [Category("decorator")]
    public abstract class decorator : action, IPixiHandler {
        // TODO: verify if exposing this to public will cause future problems
        public action [] o;

        public abstract void OnPixiEnd(pixi p);
    }
}