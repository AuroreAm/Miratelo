using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pm_camera_target_target : core
    {
        [Depend]
        m_actor ma;

        public override void Main() {}

        protected override void OnAquire()
        {
            if (!ma.target)
            Debug.LogWarning ("trying to target a target that doesn't exist");

            m_camera.o.TpsTransitionToTarget ( ma.target.md );
        }

        protected override void OnFree()
        {
            m_camera.o.TpsTransitionToTps ();
        }
    }
}
