using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class CameraWriter : Scripter
    {
        
        public Transform CameraPivot;
        public Camera Cam;

        public override void OnWrite(Character c)
        {
            m_camera cm = c.RequireModule<m_camera> ();
            cm.Coord = c.transform;
            cm.CameraPivot = CameraPivot;
            cm.Cam = Cam;
        }
        
    }
}
