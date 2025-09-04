using System.Collections;
using System.Collections.Generic;
using log4net.Filter;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class tps_data : shard
    {
        public float RotX;
        public float RotY;
        public Vector3 Rot => new Vector3(RotX, RotY, 0);

        public d_dimension_meta Subject { get; private set; }

        public void SetSubject ( d_dimension_meta subject )
        {
            Subject = subject;
        }
    }

    public abstract class tps_shot : camera_shot
    {
        [harmony]
        protected tps_data TpsData;

        protected const float radius = .5f;
        public Vector3 Offset { get; protected set; }
        public float Height { get; protected set; }
        public float Distance { get; protected set; }

        protected void SetCamPosToTps (  )
        {
            float RayDistance = Distance;
            Vector3 TargetPos = TpsData.Subject.Position + Offset + Height * Vector3.up;

            if ( Physics.SphereCast ( TpsData.Subject.Position, radius, Vecteur.LDir(TpsData.Rot,Vector3.back), out RaycastHit hit, Distance, Vecteur.Solid ) )
                RayDistance = hit.distance - 0.05f;

            CamPos = TargetPos + Vecteur.LDir(TpsData.Rot,Vector3.back) * RayDistance;
            CamRot = Quaternion.Euler(TpsData.Rot);
        }
    }

    public class tps_normal : tps_shot
    {
        protected override void awaken()
        {
            Height = TpsData.Subject.Height + .25f;
            Distance = 4;
        }

        protected override void alive()
        {
            // rotate using the mouse
            // TODO: add sensitivity tweak, add inverted mouse
            TpsData.RotY += Player.DeltaMouse.x;
            TpsData.RotX -= Player.DeltaMouse.y;
            TpsData.RotX = Mathf.Clamp( TpsData.RotX, -65, 65 );

            SetCamPosToTps ();
        }
    }
}