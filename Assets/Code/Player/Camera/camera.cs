using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [star (order.camera)]
    [inked]
    public class camera : star.main
    {
        public static camera o;

        [link]
        public hit_pause hit_pause;

        Transform coord;
        Camera unity_camera; public static Camera cam => o.unity_camera;
        CameraShot.shot shot;

        public static Quaternion get_billboard_rotation ( Vector3 position ) {
            return Quaternion.LookRotation ( o.unity_camera.transform.position - position );
        }

        public class ink : ink <camera>
        {
            public ink ( Camera camera, Transform coord )
            {
                o.coord = coord;
                o.unity_camera = camera;
            }
        }

        protected override void _ready()
        {
            o = this;
            phoenix.core.execute (this);
            set_shot (dummy);
        }

        protected override void _step()
        {
            coord.position = shot.pos;
            coord.rotation = shot.rot;
            unity_camera.fieldOfView = shot.fov;
        }

        void set_shot ( CameraShot.shot _shot )
        {
            if ( shot != null )
                unlink ( shot );

            shot = _shot;
            link ( shot );
        }

        // public methods
        // camera control
        [link]
        CameraShot.dummy dummy;
        [link]
        tps tps;
        [link]
        normal tps_normal;
        [link]
        target tps_target;
        [link]
        CameraShot.subject subject_shot;

        public void start_player_camera ( character player )
        {
            tps.player = player;
            set_shot ( tps );
        }

        public void transition_tps ()
        {
            tps.transition_to ( tps_normal );
        }

        public void transition_target ( character target )
        {
            tps_target.target_subject = target;
            tps.transition_to ( tps_target );
        }

        public void cut_to ( )
        {
            set_shot ( subject_shot );
        }

        // Screen ray
        Ray screen_ray;
        public Vector3 screen_center_world_position ( GameObject _exclude )
        {
            screen_ray.origin = coord.position;
            screen_ray.direction = coord.forward;

            int d = _exclude.layer;
            _exclude.layer = 0;
            RaycastHit hit;

            bool hit_something = Physics.Raycast(screen_ray, out hit,
            256, vecteur.SolidHit);

            _exclude.layer = d;

            if (hit_something)
                return hit.point;
            else
                return coord.position + coord.forward * 256;
        }
    }

    namespace CameraShot
    {
        [star (order.camera_shot)]
        public abstract class base_shot : star.main
        {}

        public abstract class shot : base_shot
        {
            public Vector3 pos;
            public Quaternion rot;
            public float fov = 60;
        }

        public class dummy : shot
        {
            protected override void _step()
            {
                pos = Vector3.zero;
                rot = Quaternion.identity;
            }
        }
    }
}