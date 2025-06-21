using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class CameraWriter : Scripter
    {

        public override void OnWrite(Character c)
        {
            m_camera mc = c.RequireModule<m_camera> ();
            mc.Coord = c.transform;
            mc.Cam = c.transform.GetChild(0).GetComponent<Camera>();
        }
        
    }
}
