using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // character controller movement using Unity's CharacterController
    public class m_capsule_character_controller : core
    {
        [Depend]
        public m_ground_data mgd;
        [Depend]
        m_dimension md;
        [Depend]
        m_skin ms;

        int CurrentFrame;

        /// <summary>
        /// direction the character is commanded to move in
        /// </summary>
        public Vector3 dir;

        public Transform Coord;
        public CharacterController CCA;

        public override void Create()
        {
            Coord = character.transform;
            CCA = character.gameObject.AddComponent<CharacterController>();
            UpdateCCADimension ();
        }

        void UpdateCCADimension()
        {
            CCA.height = md.h;
            CCA.radius = md.r;
            CCA.center = new Vector3 (0, md.h / 2, 0);
        }

        protected override void OnAquire()
        {
            // don't reset anything if this is aquired/freed on the same frame or next frame
            if (Time.frameCount == CurrentFrame || Time.frameCount == CurrentFrame + 1)
                return;

            dir = Vector3.zero;
        }

        protected override void OnFree()
        {
            CurrentFrame = Time.frameCount;
        }

        public override void Main()
        {
            // update dimension every frame
            // TODO: check for performance, this is mainly used for crouching, maybe use event instead
            UpdateCCADimension ();
            MoveCharacterController ();
        }

        void MoveCharacterController()
        {
            if (ms.allowMoving)
            dir += ms.GetSpdCurves() * ms.SkinDir * Time.deltaTime;


            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, true);
            CCA.Move(dir);
            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, false);

            dir = Vector3.zero;
        }
    }

    public class m_gravity_mccc : core
    {
        [Depend]
        m_ground_data mgd;

        [Depend]
        m_capsule_character_controller mccc;

        [Depend]
        m_dimension md;

        public override void Create()
        {
            mass = md.m;
        }

        public float mass;
        public float gravity;

        public override void Main()
        {
            // add gravity force // limit falling velocity when it reach terminal velocity
            if (gravity > -1000)
            gravity += Physics.gravity.y * Time.deltaTime/*a*/ * mass;

            if (mgd.onGroundAbs && gravity < 0 && Vector3.Angle (Vector3.up, mgd.groundNormal) <= 45)
            gravity = -0.2f;

            Vector3 GravityForce = new Vector3( 0, gravity * Time.deltaTime, 0 );

            // TODO: fix character can't fall when there's another character on the ground
            if ( Vector3.Angle (Vector3.up, mgd.groundNormal) > 45 )
            {
                GravityForce = new Vector3 ( mgd.groundNormal.x,- mgd.groundNormal.y, mgd.groundNormal.z ) * GravityForce.magnitude;
                mgd.groundNormal = Vector3.up;
            }

            mccc.dir += GravityForce;
        }
    }
}
