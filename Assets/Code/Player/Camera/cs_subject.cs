using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    // standard camera shot for cinematics
    public class cs_subject : camera_shot
    {
        cs_subject_data data;
        protected override void Step()
        {
            CamPos = data.GetPos;
            CamRot = data.GetRot;
            CamFoV = data.fieldOfView;
        }
    }

    [Serializable]
    public struct cs_subject_data
    {
        public Vector3 focalPoint;
        public float distance;
        public float fieldOfView;
        public float roll;
        public Vector3 RotY;
        public Vector3 offset;
        public Vector3 rotYOffset;

        public Vector3 GetPos => Vecteur.LDir (RotY, offset) + focalPoint + Vecteur.LDir(RotY,Vector3.back) * distance;
        public Quaternion GetRot => Quaternion.Euler (new Vector3 ( RotY.x + rotYOffset.x, RotY.y + rotYOffset.y, rotYOffset.z + roll));
    }
}