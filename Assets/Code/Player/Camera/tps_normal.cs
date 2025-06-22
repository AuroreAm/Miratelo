using System.Collections;
using System.Collections.Generic;
using log4net.Filter;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class tps_data : module
    {
        public Vector3 rotY;
        public m_dimension Subject { get; private set; }
        public m_actor SubjectActor { get; private set; }

        public void SetSubject ( m_actor Actor )
        {
            SubjectActor = Actor;
            Subject = Actor.md;
        }
    }

    public abstract class tps_shot : camera_shot
    {
        [Depend]
        protected tps_data td;

        protected const float radius = .5f;
        public Vector3 offset { get; protected set; }
        public float height { get; protected set; }
        public float distance { get; protected set; }

        protected void RayCameraPosition () => RayCameraPosition (td.rotY);
        protected void RayCameraPosition ( Vector3 RotY )
        {
            float RayDistance = distance;
            Vector3 TargetPos = td.Subject.position + offset + height * Vector3.up;

            if ( Physics.SphereCast ( td.Subject.position, radius, Vecteur.LDir(RotY,Vector3.back), out RaycastHit hit, distance, Vecteur.Solid ) )
                RayDistance = hit.distance - 0.05f;

            CamPos = TargetPos + Vecteur.LDir(RotY,Vector3.back) * RayDistance;
            CamRot = Quaternion.Euler(RotY);
        }
    }



    public class tps_normal : tps_shot
    {
        protected override void OnAquire()
        {
            height = td.Subject.h;
            distance = 4;
        }

        public override void Main()
        {
            // rotate using the mouse
            // TODO: add sensitivity tweak, add inverted mouse
            td.rotY.y += Player.DeltaMouse.x * 3;
            td.rotY.x -= Player.DeltaMouse.y * 3;
            td.rotY.x = Mathf.Clamp(td.rotY.x, -65, 65);

            RayCameraPosition ();
        }
    }
}