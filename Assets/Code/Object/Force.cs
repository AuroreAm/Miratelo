using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public enum ForceType { diffuse, perce }

    public struct Force
    {
        public ForceType type;
        public float raw;
        public Vector3 point;
        public Vector3 direction;

        public Force(ForceType type, float raw, Vector3 point, Vector3 direction)
        {
            this.type = type;
            this.raw = raw;
            this.point = point;
            this.direction = direction;
        }
    }
}