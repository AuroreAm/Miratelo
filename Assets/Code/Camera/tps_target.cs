using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class tps_target : tps_shot
    {
        float rotYOffset;
        float rotXOffset;

        public d_dimension target;

        Vector3 spos => td.Subject.position;
        Vector3 tpos => target.position;
        protected override void Start()
        {
            height = td.Subject.h;
            rotYOffset = Vecteur.RotDirectionY ( spos, tpos ) + 14;
            rotXOffset = Mathf.DeltaAngle(0, rotXOffset);
            yPrevious = Vecteur.RotDirectionY ( spos, tpos );
            td.rotY = rotYOffset;
            td.rotX = rotXOffset;
        }

        float yPrevious;
        protected override void Step()
        {
            // rotate offset according to mouse
            rotYOffset += Player.DeltaMouse.x ;
            rotXOffset -= Player.DeltaMouse.y;
            rotXOffset = Mathf.Clamp(rotXOffset, -65, 65);

            float AngleDiff = Mathf.DeltaAngle(yPrevious, Vecteur.RotDirectionY(spos, tpos));
            if ( Mathf.Abs ( Mathf.DeltaAngle(0, AngleDiff) ) < 360*Time.unscaledDeltaTime )
                rotYOffset += AngleDiff;
            yPrevious = Vecteur.RotDirectionY(spos, tpos);

            td.rotY = rotYOffset;
            td.rotX = rotXOffset;

            CalculateOffest();
            RayCameraPosition();
        }

        // calculate the subject offset to make sure both subject and target are in the shot
        void CalculateOffest()
        {
            float TargetDistance = Vector3.Distance(spos, tpos);
            float distRatio = Mathf.Abs(Mathf.DeltaAngle(Vecteur.RotDirection(spos, tpos).y, td.rotY)) / 180;

            distance = Mathf.Lerp ( distance, 4 + (TargetDistance * distRatio), .1f );
            offset = Vector3.Lerp ( offset, Vecteur.LDir(Vecteur.RotDirectionQuaternion(spos, tpos), Vector3.forward * (TargetDistance * distRatio / 2)), .1f );
        }
    }
}