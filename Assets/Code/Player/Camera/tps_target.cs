using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    
    public class tps_target : pc_camera_tps_controller
    {
        public Vector3 rotYOffset;
        Vector3 rotYToTarget;

        Vector3 mpos => c.C.md.position;
        Vector3 tpos => c.C.target.md.position;

        public override void Default()
        {
            SyncWithTps ();

            CalculateOffest ();
            rotYToTarget = Vecteur.RotDirection (mpos, tpos);
            rotY = rotYToTarget;
            rotYOffset = Vector3.zero;
        }

        
        public override void Update()
        {
            height = c.C.md.h;

            // rotate offset according to mouse
            rotYOffset.y += Player.DeltaMouse.x * 3;
            rotYOffset.x -= Player.DeltaMouse.y * 3;

            CalculateOffest ();

            var a = Vecteur.RotDirection (mpos, tpos);
            rotYToTarget.x = Mathf.MoveTowardsAngle ( rotYToTarget.x, a.x, 180*Time.unscaledDeltaTime );
            rotYToTarget.y = Mathf.MoveTowardsAngle ( rotYToTarget.y, a.y, 180*Time.unscaledDeltaTime );

            rotY = rotYToTarget + rotYOffset;
            rotY.x = Mathf.Clamp( Mathf.DeltaAngle (0,rotY.x), -65, 65 ); 

            return;
        }

        void CalculateOffest ()
        {
            float dist = Vector3.Distance ( mpos, tpos );
            float distRatio = Mathf.Abs (Mathf.DeltaAngle ( Vecteur.RotDirection (mpos, tpos).y, rotY.y )) / 180;

            distance = 4 + (dist * distRatio);

            offset = Vecteur.LDir ( Vecteur.RotDirection (mpos, tpos), Vector3.forward * (dist * distRatio / 2) );
        }
    }
    
}
