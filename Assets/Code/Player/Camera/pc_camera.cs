using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pcc_target_target : action
    {
        [Depend]
        m_actor ma;

        protected override bool Step()
        {
            if (!ma.target)
            Debug.LogWarning ("trying to target a target that doesn't exist");

            m_camera.o.TpsTransitionToTarget ( ma.target.md );
            return true;
        }
    }
}
