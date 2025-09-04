using System;
using UnityEngine;

namespace Triheroes.Code
{
    // standard camera shot for cinematics
    public class cs_subject : camera_shot
    {
        cs_subject_data _data;
        protected override void alive()
        {
            CamPos = _data.GetPos;
            CamRot = _data.GetRot;
            CamFoV = _data.FieldOfView;
        }
    }

    [Serializable]
    public struct cs_subject_data
    {
        public Vector3 FocalPoint;
        public float Distance;
        public float FieldOfView;
        public float Roll;
        public Vector3 RotY;
        public Vector3 Offset;
        public Vector3 RotYOffset;

        public Vector3 GetPos => Vecteur.LDir (RotY, Offset) + FocalPoint + Vecteur.LDir(RotY,Vector3.back) * Distance;
        public Quaternion GetRot => Quaternion.Euler (new Vector3 ( RotY.x + RotYOffset.x, RotY.y + RotYOffset.y, RotYOffset.z + Roll));
    }
}