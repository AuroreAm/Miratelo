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

        public bool UseGravity = true;
        public float verticalVelocity;
        /// <summary>
        /// velocity force direction applied to the character
        /// </summary>
        public Vector3 velocityDir;
        /// <summary>
        /// direction the character is commanded to move in
        /// </summary>
        public Vector3 dir;
        public float mass;

        public Transform Coord;
        public CharacterController CCA;

        public override void Create()
        {
            Coord = character.transform;
            CCA = character.gameObject.AddComponent<CharacterController>();
            UpdateCCADimension ();
            mass = md.m;
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

            verticalVelocity = 0;
            velocityDir = Vector3.zero;
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

            if (velocityDir.sqrMagnitude > 0)
                velocityDir = Vector3.MoveTowards(velocityDir, Vector3.zero, Vecteur.Drag * Time.deltaTime/*a*/);

            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, true);
            CCA.Move(dir + velocityDir);
            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, false);

            dir = Vector3.zero;
        }
    }
}
