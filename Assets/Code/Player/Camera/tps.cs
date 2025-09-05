using Lyra;
using UnityEngine;
using Triheroes.Code.CameraShot;

namespace Triheroes.Code.TPS
{
    public class data : moon
    {
        public float rotx;
        public float roty;
        public Vector3 Rot => new Vector3(rotx, roty, 0);

        public dimension subject { get; private set; }

        public void SetSubject ( dimension _subject )
        {
            subject = _subject;
        }
    }

    public abstract class shot : CameraShot.shot
    {
        [link]
        protected data data;
        protected const float r = .5f;
        public Vector3 offset { get; protected set; }
        public float distance { get; protected set; }
        public float h { get; protected set; }

        protected void pos_to_tps (  )
        {
            float ray_distance = distance;
            Vector3 target_pos = data.subject.position + offset + h * Vector3.up;

            if ( Physics.SphereCast ( data.subject.position, r, vecteur.ldir(data.Rot,Vector3.back), out RaycastHit hit, distance, vecteur.Solid ) )
                ray_distance = hit.distance - 0.05f;

            pos = target_pos + vecteur.ldir(data.Rot,Vector3.back) * ray_distance;
            rot = Quaternion.Euler(data.Rot);
        }
    }

    public class normal : shot
    {
        protected override void _start()
        {
            h = data.subject.h + .25f;
            distance = 4;
        }

        protected override void _step()
        {
            // rotate using the mouse
            // TODO: add sensitivity tweak, add inverted mouse
            data.roty += player.delta_mouse.x;
            data.rotx -= player.delta_mouse.y;
            data.rotx = Mathf.Clamp( data.rotx, -65, 65 );

            pos_to_tps ();
        }
    }
}