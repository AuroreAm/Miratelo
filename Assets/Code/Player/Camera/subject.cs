using System;
using UnityEngine;

namespace Triheroes.Code.CameraShot
{
    // standard camera shot for cinematics
    public class subject : shot
    {
        data _data;
        protected override void _step()
        {
            pos = _data.GetPos;
            rot = _data.GetRot;
            fov = _data.fov;
        }
        
        [Serializable]
        public struct data
        {
            public Vector3 focal_point;
            public float distance;
            public float fov;
            public float roll;
            public Vector3 rot;
            public Vector3 offset;
            public Vector3 roty_offset;

            public Vector3 GetPos => vecteur.ldir (rot, offset) + focal_point + vecteur.ldir(rot,Vector3.back) * distance;
            public Quaternion GetRot => Quaternion.Euler (new Vector3 ( rot.x + roty_offset.x, rot.y + roty_offset.y, roty_offset.z + roll));
        }
    }
}