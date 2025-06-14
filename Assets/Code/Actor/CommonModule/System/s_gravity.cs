using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;


namespace Triheroes.Code
{
    public class s_ccc_gravity : CustomCoreSystem<m_capsule_character_controller>
    {
        protected override void Main(m_capsule_character_controller o)
        {
            if (!o.UseGravity)
            return;

            o.verticalVelocity += Physics.gravity.y * Time.deltaTime/*a*/ * o.mass;

            if (o.mgd.onGroundAbs && o.verticalVelocity < 0 && Vector3.Angle (Vector3.up, o.mgd.groundNormal) <= 45)
            o.verticalVelocity = -0.2f;

            Vector3 GravityForce = new Vector3( 0, o.verticalVelocity * Time.deltaTime, 0 );

            // TODO: fix character can't fall when there's another character on the ground
            if ( Vector3.Angle (Vector3.up, o.mgd.groundNormal) > 45 )
            {
                GravityForce = new Vector3 ( o.mgd.groundNormal.x,-o.mgd.groundNormal.y,o.mgd.groundNormal.z ) * GravityForce.magnitude;
                o.mgd.groundNormal = Vector3.up;
            }

            o.dir += GravityForce;
        }
    }
}
