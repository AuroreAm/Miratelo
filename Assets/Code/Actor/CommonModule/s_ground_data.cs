using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_ground_data_ccc : pixi
    {
        [Depend]
        s_capsule_character_controller sccc;

        [Depend]
        d_ground_data dgd;

        protected override void Step()
        {
            dgd.onGroundAbs = false;
            dgd.onGround = Physics.SphereCast(sccc.Coord.position + new Vector3(0, sccc.CCA.radius + 0.1f, 0), sccc.CCA.radius, Vector3.down, out RaycastHit hit, 0.5f, Vecteur.SolidCharacter);

            if (dgd.onGround)
            {
                dgd.groundNormal = hit.normal;
                dgd.onGroundAbs = hit.distance <= 0.2f;
            }
        }
    }
}
