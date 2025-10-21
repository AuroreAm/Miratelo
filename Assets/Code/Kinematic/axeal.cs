using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code.Axeal {
    // kinematic physics
    [inked]
    public class axeal : controller, gold <push> {
        public float m { get; private set; }
        public float mu => m * 10; // kg to mu ( 0.1 kg )

        [link]
        capsule capsule;
        [link]
        gravity gravity;
        [link]
        ground ground;

        [link]
        skin_dir skin_dir; 

        /// <summary> all force, active and inactive on the axeal, for internal use,
        /// this is public accessible for special case tweak
        /// don't mess with the reference
        /// e.g. for jump to finish curve early </summary>
        public force_curve [] forces = new force_curve [2];
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
            if (force_count + 1 >= forces.Length) {
                var new_forces = new List <force_curve> ( forces );
                new_forces.Add ( new force_curve () );
                forces = new_forces.ToArray ();
            }

            forces [force_count] = new force_curve ( _data );
            force_count++;
        }

        public force_curve main_force => forces [0];
        public void stop_main_force () => forces [0].stop();

        public void deviate_main_force ( Vector3 _dir ) => forces [0].deviate ( _dir );

        public static Vector3 slope_projection ( Vector3 dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (dir, GroundNormal).normalized * dir.magnitude;

        public void _radiate(push gleam) {
            add_force ( new force_curve_data ( gleam.dir_per_mu / mu , .5f, push.force ) );
        }
    }

    public struct push {
        public Vector3 dir_per_mu;
        public static AnimationCurve force = res.curves.q ( sh.force );

        public push ( Vector3 dir_per_mu ) {
            this.dir_per_mu = dir_per_mu;
        }
    }
}