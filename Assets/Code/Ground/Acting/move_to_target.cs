using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Axeal;
using UnityEngine;
using UnityEngine.AI;

namespace Triheroes.Code
{
    [path ("acting")]
    public class move_to_target : move_to
    {
        [link]
        warrior warrior;

        [link]
        capsule capsule;

        protected override float get_offset_stop_distance() {
            return capsule.r + warrior.target.system.get <capsule> ().r;
        }

        protected override Vector3 get_target() {
            return warrior.target.c.position;
        }
    }
}