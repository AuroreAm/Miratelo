using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code.Axeal {
    // character controller physics
    // kinematic physics
    [inked]
    public class axeal : controller {
        public float m { get; private set; }

        [link]
        capsule capsule;
        [link]
        gravity gravity;
        [link]
        ground ground;

        [link]
        skin_dir skin_dir; 

        force_curve [] forces = new force_curve [2];
        int force_count;

        public class ink : ink <axeal> {
            public ink ( float m ) { o.m = m; }
        }

        protected override void _ready() {
            phoenix.core.execute (this);
        }

        protected override void _start() {
            link (capsule);
            link (gravity);
            link (ground);
        }

        protected override void _step() {

            if (skin_dir.on)
            capsule.move ( skin_dir.dir * skin_dir.delta );

            bool stop_gravity = false;

            for (int i = 0; i < force_count; i++) {
                capsule.move ( forces [i].tick () );

                if (forces [i].flag == 1)
                    stop_gravity = true;
            }

            while ( force_count > 0 && forces [force_count - 1].done ) {
                force_count --;
            }

            if ( stop_gravity && gravity.on ) {
                unlink ( gravity );
            }
            if (!stop_gravity && !gravity.on) {
                link ( gravity );
            }

        }

        public void walk ( Vector3 dir_s ) {
            capsule.move ( Time.deltaTime * slope_projection ( dir_s, ground.normal ) );
        }

        public void move ( Vector3 dir_s ) {
            capsule.move ( Time.deltaTime * dir_s );
        }

        public void set_force (force_curve_data _data) {
            forces [0] = new force_curve ( _data );
            force_count = 1;
        }

        public void set_forces (force_curve_data [] _data) {
            if ( _data.Length > forces.Length )
                forces = new force_curve [_data.Length];

            for (int i = 0; i < _data.Length; i++) 
                forces [i] = new force_curve ( _data [i] );

            force_count = _data.Length;
        }

        public void add_force (force_curve_data _data) {
            if (force_count + 1 >= forces.Length)
                forces = new force_curve [force_count + 1];

            forces [force_count] = new force_curve ( _data );
            force_count++;
        }

        public force_curve main_force => forces [0];
        public void stop_main_force () => forces [0].stop();

        public void deviate_main_force ( Vector3 _dir ) => forces [0].deviate ( _dir );

        public static Vector3 slope_projection ( Vector3 Dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (Dir, GroundNormal).normalized * Dir.magnitude;
    }
}