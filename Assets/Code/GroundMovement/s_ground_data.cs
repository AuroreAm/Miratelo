using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [SysBase (SysOrder.s_ground_data_ccc)]
    public class s_ground_data_ccc : sys.ext
    {
        [Link]
        s_capsule_character_controller capsule;

        [Link]
        d_ground_data dgd;

        protected override void OnStep()
        {
            dgd.onGroundAbs = false;
            dgd.onGround = Physics.SphereCast (
                capsule.Coord.position + new Vector3(0, capsule.UnityCharacterController.radius + 0.1f, 0),
                capsule.UnityCharacterController.radius, Vector3.down,
                out RaycastHit hit,
                0.5f,
                Vecteur.SolidCharacter );

            if (dgd.onGround)
            {
                dgd.groundNormal = hit.normal;
                dgd.onGroundAbs = hit.distance <= 0.2f;
            }
        }
    }
}
