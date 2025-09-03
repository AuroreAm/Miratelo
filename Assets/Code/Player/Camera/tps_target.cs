using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class tps_target : tps_shot
    {
        float _rotYOffset;
        float _rotXOffset;

        public d_dimension_meta Target;

        Vector3 Spos => TpsData.Subject.Position;
        Vector3 Tpos => Target.Position;
        protected override void OnStart()
        {
            Height = TpsData.Subject.Height;
            _rotYOffset = Vecteur.RotDirectionY ( Spos, Tpos ) + 14;
            _rotXOffset = Mathf.DeltaAngle(0, _rotXOffset);
            _yPrevious = Vecteur.RotDirectionY ( Spos, Tpos );
            TpsData.RotY = _rotYOffset;
            TpsData.RotX = _rotXOffset;
        }

        float _yPrevious;
        protected override void OnStep()
        {
            // rotate offset according to mouse
            _rotYOffset += Player.DeltaMouse.x ;
            _rotXOffset -= Player.DeltaMouse.y;
            _rotXOffset = Mathf.Clamp( _rotXOffset, -65, 65 );

            float AngleDiff = Mathf.DeltaAngle( _yPrevious, Vecteur.RotDirectionY ( Spos, Tpos ) );
            if ( Mathf.Abs ( Mathf.DeltaAngle ( 0, AngleDiff ) ) < 360*Time.unscaledDeltaTime )
                _rotYOffset += AngleDiff;
            _yPrevious = Vecteur.RotDirectionY ( Spos, Tpos );

            TpsData.RotY = _rotYOffset;
            TpsData.RotX = _rotXOffset;

            CalculateOffest();
            SetCamPosToTps();
        }

        // calculate the subject offset to make sure both subject and target are in the shot
        void CalculateOffest()
        {
            float TargetDistance = Vector3.Distance ( Spos, Tpos );
            float distRatio = Mathf.Abs ( Mathf.DeltaAngle ( Vecteur.RotDirection( Spos, Tpos ).y, TpsData.RotY ) ) / 180;

            Distance = Mathf.Lerp ( Distance, 4 + (TargetDistance * distRatio), .1f );
            Offset = Vector3.Lerp ( Offset, Vecteur.LDir(Vecteur.RotDirectionQuaternion( Spos, Tpos ), Vector3.forward * ( TargetDistance * distRatio / 2 ) ), .1f );
        }
    }
}