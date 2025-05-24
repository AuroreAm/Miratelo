using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_ccc_ground_data : CustomCoreSystem<m_capsule_character_controller>
    {
        protected override void Main(m_capsule_character_controller o)
        {
            if (!o.UseGravity)
                return;

            o.mgd.onGroundAbs = false;
            o.mgd.onGround = Physics.SphereCast(o.Coord.position + new Vector3(0, o.CCA.radius + 0.1f, 0), o.CCA.radius, Vector3.down, out RaycastHit hit, 0.5f, Vecteur.SolidCharacter);

            if (o.mgd.onGround)
            {
                o.mgd.groundNormal = hit.normal;
                o.mgd.onGroundAbs = hit.distance <= 0.2f;
            }
        }
    }
}
