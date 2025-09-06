using System;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [star (order.camera)]
    [inked]
    public class camera : star
    {
        public static camera o { private set; get; }
        public Transform coord {private set; get;}
        Camera unity_camera;
        CameraShot.shot shot;

        public static explicit operator Camera ( camera o ) => o.unity_camera;

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
            phoenix.core.start (this);
            set_shot(dummy);
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
                this.unlink ( shot );

            shot = _shot;
            this.link ( shot );
        }

        // public methods
        // camera control
        [link]
        public TPS.data tps;
        [link]
        CameraShot.dummy dummy;
        [link]
        TPS.normal normal_shot;
        [link]
        tps_transition transition_shot;
        [link]
        TPS.target target_shot;
        [link]
        CameraShot.subject subject_shot;

        public void tps_a_character ( dimension character )
        {
            tps.SetSubject ( character );
            set_shot ( normal_shot );
        }

        public void transition_tps ()
        {
            if (shot is TPS.shot s)
            {
                transition_shot.Set(s, normal_shot);
                set_shot(transition_shot);
            }
        }

        public void transition_target ( dimension target )
        {
            target_shot.target_subject = target;
            if (shot is TPS.shot s)
            {
                transition_shot.Set(s, target_shot);
                set_shot(transition_shot);
            }
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
            256, vecteur.SolidCharacter);

            _exclude.layer = d;

            if (hit_something)
                return hit.point;
            else
                return coord.position + coord.forward * 256;
        }


        // built in shots
        public sealed class tps_transition : TPS.shot
        {
            float in_roty;
            float in_rotx;
            float in_h;
            float in_distance;
            Vector3 in_offset;
            TPS.shot @in;
            TPS.shot @out;

            const float radius = .5f;
            public Vector3 transtion_offset { get; private set; }
            public float transition_height { get; private set; }
            public float transition_distance { get; private set; }

            [link]
            new TPS.data data;

            public void Set(TPS.shot _in, TPS.shot _out)
            {
                if (_in == null || _out == null)
                    throw new NullReferenceException("tps_transition: null tps_shot");
                @in = _in;
                @out = _out;
            }

            float roty;
            float rotx;

            protected override void _start()
            {
                in_roty = data.roty;
                in_rotx = data.rotx;
                in_h = @in.h;
                in_distance = @in.distance;
                in_offset = @in.offset;

                this.link ( @out );
                t = 0;
            }

            float t;
            protected override void _step()
            {
                t = Mathf.Lerp(t, 1, .1f);

                rotx = Mathf.LerpAngle ( in_rotx, data.rotx, t );
                roty = Mathf.LerpAngle ( in_roty, data.roty, t );
                transtion_offset = Vector3.Lerp ( in_offset, @out.offset, t );
                transition_height = Mathf.Lerp ( in_h, @out.h, t );
                transition_distance = Mathf.Lerp ( in_distance, @out.distance, t );

                pos_to_tps();

                if (t >= .95f)
                    o.set_shot ( @out );
            }

            protected override void _stop()
            {
                @in = null;
                @out = null;
            }

            new void pos_to_tps()
            {
                float ray_distance = transition_distance;
                Vector3 target_pos = data.subject.position + transtion_offset + transition_height * Vector3.up;

                if ( Physics.SphereCast(data.subject.position, radius, vecteur.ldir( new Vector3(rotx, roty,0), Vector3.back ), out RaycastHit hit, transition_distance, vecteur.Solid) )
                    ray_distance = hit.distance - 0.05f;

                pos = target_pos + vecteur.ldir ( new Vector3(rotx, roty,0), Vector3.back ) * ray_distance;
                rot = Quaternion.Euler ( new Vector3(rotx, roty,0) );
            }
        }
    }

    namespace CameraShot
    {
        [star (order.camera_shot)]
        public abstract class shot : star.main
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