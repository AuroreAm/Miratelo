using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pm_camera_target_target : pixi
    {
        [Depend]
        d_actor ma;

        protected override void Start()
        {
            if (!ma.target)
            Debug.LogWarning ("trying to target a target that doesn't exist");

            s_camera.o.TpsTransitionToTarget ( ma.target.dd );
        }

        protected override void Stop()
        {
            s_camera.o.TpsTransitionToTps ();
        }
    }
}
