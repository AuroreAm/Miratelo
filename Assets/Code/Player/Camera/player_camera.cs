using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using Triheroes.Code.CameraShot;
using Triheroes.Code.Axeal;

namespace Triheroes.Code
{
    public class player_camera : shot
    {
        public character player;
        float rotx;
        float roty;
        float distance;
        float h;
        Vector3 offset;

        [link]
        normal normal;

        tps_shot shot;
        tps_shot transition_from;
        float t;

        public Vector3 rot2 => new Vector3(rotx, roty, 0);

        protected override void _start()
        {
            shot = normal;
            link (normal);

            ray_distance = distance;
        }

        float ray_distance;
        const float distance_speed = 5;
        const float r = .1f;
        const float r2 = .5f;
        Collider [] colliders = new Collider[1];
        void tps_pos ()
        {
            float distance = this.distance;
            Vector3 target_pos = player.position + offset + h * Vector3.up;

            if ( Physics.SphereCast ( target_pos, r, vecteur.ldir( rot2, Vector3.back ), out RaycastHit hit, distance, vecteur.Static ) )
                distance = hit.distance;

            if (Physics.OverlapSphereNonAlloc ( target_pos + vecteur.ldir( rot2,Vector3.back ) * distance, r2, colliders, vecteur.Decor ) > 0)
                distance -= r2;

            if ( distance < ray_distance )
            ray_distance = distance;
            else
            ray_distance = Mathf.Lerp ( ray_distance, distance, Time.deltaTime * distance_speed );

            pos = target_pos + vecteur.ldir( rot2,Vector3.back ) * ray_distance;
            rot = Quaternion.Euler( rot2 );
        }

        protected override void _step()
        {
            if ( transition_from != null )
            follow_transition ();
            else follow_shot ();
            tps_pos ();
        }

        public void transition_to ( tps_shot _shot )
        {
            transition_from = shot;
            unlink (shot);
            t = 0;

            shot = _shot;
            link (shot);
        }

        void follow_shot ()
        {    
            rotx = shot.rotx;
            roty = shot.roty;
            distance = shot.distance;
            h = shot.h;
            offset = shot.offset;
        }

        void follow_transition ()
        {
            t = Mathf.Lerp (t, 1, .1f);
            rotx = Mathf.LerpAngle(transition_from.rotx, shot.rotx, t);
            roty = Mathf.LerpAngle(transition_from.roty, shot.roty, t);

            offset = Vector3.Lerp(transition_from.offset, shot.offset, t);
            h = Mathf.Lerp(transition_from.h, shot.h, t);
            distance = Mathf.Lerp(transition_from.distance, shot.distance, t);

            if (t >= .95f)
                transition_from = null;
        }
    }

    public abstract class tps_shot : base_shot
    {
        [link]
        protected player_camera cam;

        public float rotx {protected set; get;}
        public float roty {protected set; get;}
        public float distance {protected set; get;}
        public float h {protected set; get;}
        public Vector3 offset {protected set; get;}
    }

    public class player_camera_target : controller
    {
        [link]
        warrior warrior;

        protected override void _start()
        {
            if (!warrior.target)
            Debug.LogWarning ("trying to target a target that doesn't exist");

            camera.o.transition_target ( warrior.target.c );
        }

        protected override void _stop()
        {
            camera.o.transition_tps ();
        }
    }
}
