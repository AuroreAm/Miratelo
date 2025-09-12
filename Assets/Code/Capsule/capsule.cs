using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // character controller movement using Unity's CharacterController
    [ star ( order.capsule ) ]
    [inked]
    public class capsule : star.main
    {
        [link]
        character c;
        [link]
        ground_detection _ground_detection;
        [link]
        dimension dimension;
        
        [link]
        skin_dir skin_dir;

        int frame;

        /// <summary> direction the character is commanded to move in </summary>
        public Vector3 dir;
        public Transform coord;
        public CharacterController character_controller {get; private set;}

        public float m {private set; get;}
        float _h, _r;

        public class ink : ink < capsule >
        {
            public ink ( float h, float r, float m )
            {
                o._h = h;
                o._r = r;
                o.m = m;
            }
        }

        protected override void _ready ()
        {
            coord = c.coord;
            character_controller = c.gameobject.AddComponent<CharacterController>();
            character_controller.height = _h;
            character_controller.radius = _r;
            character_controller.center = new Vector3 (0, _h / 2, 0);
            form_dimension ();
        }

        void form_dimension ()
        {
            dimension.h = character_controller.height;
            dimension.r = character_controller.radius;
        }

        protected override void _start()
        {
            this.link ( _ground_detection );

            // don't reset anything if this is started/stopped on the same frame or next frame
            if (Time.frameCount == frame || Time.frameCount == frame + 1)
                return;

            dir = Vector3.zero;
        }

        protected override void _stop() => frame = Time.frameCount;

        protected override void _step()
        {
            // update meta dimension every frame
            // TODO: check for performance
            form_dimension ();
            move_character_controller ();
        }

        void move_character_controller()
        {
            if (skin_dir.on)
            dir += skin_dir.dir * skin_dir.delta;

            Physics.IgnoreLayerCollision(coord.gameObject.layer, vecteur.ATTACK, true);
            character_controller.Move(dir);
            Physics.IgnoreLayerCollision(coord.gameObject.layer, vecteur.ATTACK, false);

            dir = Vector3.zero;
        }
        
        [star (order.capsule_ground_detection)]
        public class ground_detection : main
        {
            [link]
            capsule capsule;

            [link]
            ground ground;

            protected override void _step()
            {
                ground.raw = false;
                ground.set ( Physics.SphereCast (
                    capsule.coord.position + new Vector3(0, capsule.character_controller.radius + 0.1f, 0),
                    capsule.character_controller.radius, Vector3.down,
                    out RaycastHit hit,
                    0.5f,
                    vecteur.SolidCharacter ) );

                if ( ground )
                {
                    ground.normal = hit.normal;
                    ground.raw = hit.distance <= 0.2f;
                }
            }
        }
        
        [ star ( order.capsule_gravity ) ]
        public class gravity : star.main
        {
            [link]
            ground ground;

            [link]
            capsule capsule;

            public static implicit operator float(gravity gravity) => gravity.g;

            float mass => capsule.m;
            float g;

            protected override void _step()
            {
                // add gravity force // limit falling velocity when it reach terminal velocity
                if (g > -1000)
                g += Physics.gravity.y * Time.deltaTime * mass;

                if ( ground.raw && g < 0 && Vector3.Angle (Vector3.up, ground.normal ) <= 45)
                g = -0.2f;

                Vector3 force = new Vector3( 0, g * Time.deltaTime, 0 );

                // TODO: fix character can't fall when there's another character on the ground
                if ( Vector3.Angle (Vector3.up, ground.normal) > 45 )
                {
                    force = new Vector3 ( ground.normal.x,- ground.normal.y, ground.normal.z ) * force.magnitude;
                    ground.normal = Vector3.up;
                }
                capsule.dir += force;
            }
        }
    }

}
