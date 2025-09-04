using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // character controller movement using Unity's CharacterController
    [ SysBase ( SysOrder.s_capsule_character_controller ) ]
    [NeedPackage]
    public class s_capsule_character_controller : sys.ext
    {
        [Link]
        character c;
        [Link]
        s_ground_data_ccc groundDataSys;
        [Link]
        d_dimension_meta dimensionMeta;
        [Link]
        s_skin skin;

        int _currentFrame;

        /// <summary>
        /// direction the character is commanded to move in
        /// </summary>
        public Vector3 Dir;
        public Transform Coord;
        public CharacterController UnityCharacterController {get; private set;}

        public float Mass {private set; get;}
        float _initialHeight, _initialRadius;

        public class package : Package < s_capsule_character_controller >
        {
            public package ( float h, float r, float m )
            {
                o._initialHeight = h;
                o._initialRadius = r;
                o.Mass = m;
            }
        }

        protected override void OnStructured ()
        {
            Coord = c.Coord;
            UnityCharacterController = c.GameObject.AddComponent<CharacterController>();
            UnityCharacterController.height = _initialHeight;
            UnityCharacterController.radius = _initialRadius;
            UnityCharacterController.center = new Vector3 (0, _initialHeight / 2, 0);
            UpdateMetaDimension ();
        }

        void UpdateMetaDimension()
        {
            dimensionMeta.Height = UnityCharacterController.height;
            dimensionMeta.Radius = UnityCharacterController.radius;
        }

        protected override void OnStart()
        {
            this.Link ( groundDataSys );

            // don't reset anything if this is started/stopped on the same frame or next frame
            if (Time.frameCount == _currentFrame || Time.frameCount == _currentFrame + 1)
                return;

            Dir = Vector3.zero;
        }

        protected override void OnStop()
        {
            _currentFrame = Time.frameCount;
        }

        protected override void OnStep()
        {
            // update meta dimension every frame
            // TODO: check for performance
            UpdateMetaDimension ();
            MoveCharacterController ();
        }

        void MoveCharacterController()
        {
            if (skin.RootOfCharacterTransform)
            Dir += skin.GetSpdCurves() * skin.SkinDir * Time.deltaTime;

            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, true);
            UnityCharacterController.Move(Dir);
            Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, false);

            Dir = Vector3.zero;
        }
    }

    [ SysBase ( SysOrder.s_gravity_ccc ) ]
    public class s_gravity_ccc : sys.ext
    {
        [Link]
        d_ground_data groundData;

        [Link]
        s_capsule_character_controller capsule;

        public float Gravity => gravity;

        float mass => capsule.Mass;
        float gravity;

        protected override void OnStep()
        {
            // add gravity force // limit falling velocity when it reach terminal velocity
            if (gravity > -1000)
            gravity += Physics.gravity.y * Time.deltaTime * mass;

            if ( groundData.onGroundAbs && gravity < 0 && Vector3.Angle (Vector3.up, groundData.groundNormal ) <= 45)
            gravity = -0.2f;

            Vector3 GravityForce = new Vector3( 0, gravity * Time.deltaTime, 0 );

            // TODO: fix character can't fall when there's another character on the ground
            if ( Vector3.Angle (Vector3.up, groundData.groundNormal) > 45 )
            {
                GravityForce = new Vector3 ( groundData.groundNormal.x,- groundData.groundNormal.y, groundData.groundNormal.z ) * GravityForce.magnitude;
                groundData.groundNormal = Vector3.up;
            }

            capsule.Dir += GravityForce;
        }
    }
}
