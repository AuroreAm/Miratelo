using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public struct Slash
    {
        public float raw;
        public Vector3 point;
        public Vector3 direction;
        public float sharpness;

        public Slash ( float raw, Vector3 point, Vector3 direction, float sharpness )
        {
            this.point = point;
            this.direction = direction;
            this.sharpness = sharpness;
            this.raw = raw;
        }
    }

    public struct Knock
    {
        public Vector3 Dir;

        public Knock ( Vector3 ForceDir )
        {
            Dir = ForceDir;
        }
    }

    public struct Perce
    {
        public Vector3 point;
        public float raw;

        public Perce ( Vector3 point, float raw )
        {
            this.point = point;
            this.raw = raw;
        }
    }
}