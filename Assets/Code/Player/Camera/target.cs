using UnityEngine;

namespace Triheroes.Code.TPS
{
    public class target : shot
    {
        float roty_offset;
        float rotx_offset;
        float roty_previous;

        public dimension target_subject;

        Vector3 spos => data.subject.position;
        Vector3 tpos => target_subject.position;
        protected override void _start()
        {
            h = data.subject.h;
            roty_offset = vecteur.rot_direction_y ( spos, tpos ) + 14;
            rotx_offset = Mathf.DeltaAngle(0, rotx_offset);
            roty_previous = vecteur.rot_direction_y ( spos, tpos );
            data.roty = roty_offset;
            data.rotx = rotx_offset;
        }

        protected override void _step()
        {
            // rotate offset according to mouse
            roty_offset += player.delta_mouse.x ;
            rotx_offset -= player.delta_mouse.y;
            rotx_offset = Mathf.Clamp( rotx_offset, -65, 65 );

            float angle_diff = Mathf.DeltaAngle( roty_previous, vecteur.rot_direction_y ( spos, tpos ) );
            if ( Mathf.Abs ( Mathf.DeltaAngle ( 0, angle_diff ) ) < 360*Time.unscaledDeltaTime )
                roty_offset += angle_diff;
            roty_previous = vecteur.rot_direction_y ( spos, tpos );

            data.roty = roty_offset;
            data.rotx = rotx_offset;

            calculate_offset ();
            pos_to_tps ();
        }

        // calculate the subject offset to make sure both subject and target are in the shot
        void calculate_offset ()
        {
            float target_distance = Vector3.Distance ( spos, tpos );
            float distance_ratio = Mathf.Abs ( Mathf.DeltaAngle ( vecteur.rot_direction( spos, tpos ).y, data.roty ) ) / 180;

            distance = Mathf.Lerp ( distance , 4 + (target_distance * distance_ratio), .1f );
            offset = Vector3.Lerp ( offset, vecteur.ldir(vecteur.rot_direction_quaternion ( spos, tpos ), Vector3.forward * ( target_distance * distance_ratio / 2 ) ), .1f );
        }
    }
}