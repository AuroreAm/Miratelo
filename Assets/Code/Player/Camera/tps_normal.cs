using System.Collections;
using System.Collections.Generic;
using log4net.Filter;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class tps_data : pix
    {
        public Vector3 rotY;
        public d_dimension Subject { get; private set; }
        public d_actor SubjectActor { get; private set; }

        public void SetSubject ( d_actor Actor )
        {
            SubjectActor = Actor;
            Subject = Actor.dd;
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

        protected void RayCameraPosition (  )
        {
            float RayDistance = distance;
            Vector3 TargetPos = td.Subject.position + offset + height * Vector3.up;

            if ( Physics.SphereCast ( td.Subject.position, radius, Vecteur.LDir(td.rotY,Vector3.back), out RaycastHit hit, distance, Vecteur.Solid ) )
                RayDistance = hit.distance - 0.05f;

            CamPos = TargetPos + Vecteur.LDir(td.rotY,Vector3.back) * RayDistance;
            CamRot = Quaternion.Euler(td.rotY);
        }
    }



    public class tps_normal : tps_shot
    {
        protected override void Start()
        {
            height = td.Subject.h + .25f;
            distance = 4;
        }

        protected override void Step()
        {
            // rotate using the mouse
            // TODO: add sensitivity tweak, add inverted mouse
            td.rotY.y += Player.DeltaMouse.x;
            td.rotY.x -= Player.DeltaMouse.y;
            td.rotY.x = Mathf.Clamp(td.rotY.x, -65, 65);

            RayCameraPosition ();
        }
    }
}