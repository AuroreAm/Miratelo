using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEditor.Experimental;
using UnityEngine;

namespace Triheroes.Code
{
    // ground data used by movement modules like m_capsule_character_controller
    public class ground : moon
    {
        bool on_ground = true;
        public bool raw;

        public void set ( bool is_grounded )
        {
            on_ground = is_grounded;
        }

        public static implicit operator bool(ground a)
        {
            return a.on_ground;
        }

        public Vector3 normal = Vector3.up;
    }
}