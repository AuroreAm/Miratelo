using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    
    /// <summary>
    /// normal camera tps controller
    /// </summary>
    public class tps_normal : pc_camera_tps_controller
    {
        public override void Default()
        {
            SyncWithTps ();
            offset = Vector3.zero;
        }

        public override void Update()
        {
            height = c.C.md.h;
            distance = 4;

            // rotate using the mouse
            // TODO: add sensitivity tweak, add inverted mouse
            rotY.y += Player.DeltaMouse.x * 3;
            rotY.x -= Player.DeltaMouse.y * 3;
            rotY.x = Mathf.Clamp(rotY.x, -65, 65);
        }
    }

    /*
    [RegisterAsBase]
    public class tps_normal : camera_shot
    {
        public override void Main()
        {
        }
    }*/
}