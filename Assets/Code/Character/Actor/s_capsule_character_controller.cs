using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // character controller movement using Unity's CharacterController
    public class s_capsule_character_controller : pixi
    {
        [Depend]
        character c;
        [Depend]
        d_dimension dd;
        [Depend]
        s_skin ss;
        [Depend]
        s_ground_data_ccc sgdc; int key_gdc;

        int CurrentFrame;

        /// <summary>
        /// direction the character is commanded to move in
        /// </summary>
        public Vector3 dir;

        public Transform Coord;
        public CharacterController CCA;

        public override void Create()
        {
            Coord = dd.Coord;
            CCA = c.gameObject.AddComponent<CharacterController>();
            UpdateCCADimension ();
        }

        void UpdateCCADimension()
        {
            CCA.height = dd.h;
            CCA.radius = dd.r;
            CCA.center = new Vector3 (0, dd.h / 2, 0);
        }

        protected override void Start()
        {
            key_gdc = Stage.Start (sgdc);

            // don't reset anything if this is started/stopped on the same frame or next frame
            if (Time.frameCount == CurrentFrame || Time.frameCount == CurrentFrame + 1)
                return;

            dir = Vector3.zero;
        }

        protected override void Stop()
        {
            CurrentFrame = Time.frameCount;

            Stage.Stop (key_gdc);
        }

        protected override void Step()
        {
            // update dimension every frame
            // TODO: check for performance, this is mainly used for crouching, maybe use event instead
            UpdateCCADimension ();
            MoveCharacterController ();
        }

        void MoveCharacterController()
        {
            if (ss.allowMoving)
            dir += ss.GetSpdCurves() * ss.SkinDir * Time.deltaTime;


            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, true);
            CCA.Move(dir);
            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, false);

            dir = Vector3.zero;
        }
    }

    public class s_gravity_ccc : pixi
    {
        [Depend]
        d_ground_data dgd;

        [Depend]
        s_capsule_character_controller sccc;

        [Depend]
        d_dimension dd;

        public override void Create()
        {
            mass = dd.m;
        }

        public float mass;
        public float gravity;

        protected override void Step()
        {
            // add gravity force // limit falling velocity when it reach terminal velocity
            if (gravity > -1000)
            gravity += Physics.gravity.y * Time.deltaTime/*a*/ * mass;

            if (dgd.onGroundAbs && gravity < 0 && Vector3.Angle (Vector3.up, dgd.groundNormal) <= 45)
            gravity = -0.2f;

            Vector3 GravityForce = new Vector3( 0, gravity * Time.deltaTime, 0 );

            // TODO: fix character can't fall when there's another character on the ground
            if ( Vector3.Angle (Vector3.up, dgd.groundNormal) > 45 )
            {
                GravityForce = new Vector3 ( dgd.groundNormal.x,- dgd.groundNormal.y, dgd.groundNormal.z ) * GravityForce.magnitude;
                dgd.groundNormal = Vector3.up;
            }

            sccc.dir += GravityForce;
        }
    }
}
