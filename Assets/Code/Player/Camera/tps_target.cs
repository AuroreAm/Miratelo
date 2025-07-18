using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class tps_target : tps_shot
    {
        [Depend]
        s_camera mc;
        Vector3 rotYOffset;

        public d_dimension target;

        Vector3 spos => td.Subject.position;
        Vector3 tpos => target.position;
        protected override void Start()
        {
            height = td.Subject.h;
            rotYOffset = Vecteur.RotDirection ( spos, tpos );
            rotYOffset.x = Mathf.DeltaAngle(0, rotYOffset.x) + 14;
            yPrevious = Vecteur.RotDirectionY ( spos, tpos );
            td.rotY = new Vector3 ( rotYOffset.x, yPrevious, 0 );
        }

        float yPrevious;
        protected override void Step()
        {
            // rotate offset according to mouse
            rotYOffset.y += Player.DeltaMouse.x * 3;
            rotYOffset.x -= Player.DeltaMouse.y * 3;
            rotYOffset.x = Mathf.Clamp(rotYOffset.x, -65, 65);

            float AngleDiff = Mathf.DeltaAngle(yPrevious, Vecteur.RotDirectionY(spos, tpos));
            if ( Mathf.Abs ( Mathf.DeltaAngle(0, AngleDiff) ) < 180*Time.unscaledDeltaTime )
                rotYOffset.y += AngleDiff;
            yPrevious = Vecteur.RotDirectionY(spos, tpos);

            td.rotY = rotYOffset;

            CalculateOffest();
            RayCameraPosition();
        }

        // calculate the subject offset to make sure both subject and target are in the shot
        void CalculateOffest()
        {
            float TargetDistance = Vector3.Distance(spos, tpos);
            float distRatio = Mathf.Abs(Mathf.DeltaAngle(Vecteur.RotDirection(spos, tpos).y, td.rotY.y)) / 180;

            distance = Mathf.Lerp ( distance, 4 + (TargetDistance * distRatio), .1f );
            offset = Vector3.Lerp ( offset, Vecteur.LDir(Vecteur.RotDirectionQuaternion(spos, tpos), Vector3.forward * (TargetDistance * distRatio / 2)), .1f );
        }
    }
}