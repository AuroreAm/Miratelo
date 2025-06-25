using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // core command between idle - walk lateral
    // uses CharacterController physics
    // manages animations
    public class c_ground_movement_lateral : controller
    {

        [Depend]
        m_capsule_character_controller mccc;
        [Depend]
        m_skin ms;
        [Depend]
        m_ground_data mgd;

        public SuperKey state;
        public Vector3 lateralDir;
        public Vector3 rotDir;

        bool firstFrame;

        protected override void OnAquire()
        {
            if (firstFrame == true)
            {
                lateralDir = Vector3.zero;
                rotDir = Vecteur.LDir ( ms.rotY, Vector3.forward );
                ToIdle ();
                firstFrame = false;
            }
            else
            {
                if (state == StateKey.idle)
                ToIdle ();
                else ToLateral ();
            }
            mccc.Aquire (this);
        }

        public override void Main()
        {
            Animation ();
            Rotation ();
            lateralDir = Vector3.zero;
        }

        protected override void OnFree()
        {
            mccc.Free (this);
        }

        void Animation ()
        {
            // idle => lateral
            if ( state == StateKey.idle && lateralDir.sqrMagnitude >= 0.01f)
            {
                ToLateral ();
                Animation ();
                return;
            }
            // lateral => idle // lateral animation
            else if ( state == StateKey.run_lateral )
            {
                Vector3 relativeDir = Vecteur.LDir( new Vector3(0, 360 - ms.rotY.y, 0), lateralDir ).normalized;
                SetAnimationDirectionFloat(relativeDir.x, relativeDir.z);

                if (lateralDir.sqrMagnitude < 0.01f)
                    ToIdle();
            }
        }

        void Rotation ()
        {
            float RotYTarget = 0;
            if (rotDir.magnitude > 0)
                RotYTarget =  Vecteur.RotDirectionY ( Vector3.zero, rotDir);

            ms.rotY = new Vector3(0, Mathf.MoveTowardsAngle(ms.rotY.y, RotYTarget, Time.deltaTime * 720), 0);
        }

        void ToLateral ()
        {
            ms.PlayState ( 0, AnimationKey.run_lateral );
            state = StateKey.run_lateral;
        }

        void ToIdle ()
        {
            ms.PlayState (0, AnimationKey.idle,0.1f);
            state = StateKey.idle;
        }

        float dx; float dz;
        public void SetAnimationDirectionFloat ( float _dx,float _dz, float GravityPerSecond = 3 )
        {
            dx = Mathf.MoveTowards ( dx, _dx, GravityPerSecond * Time.deltaTime );
            dz = Mathf.MoveTowards ( dz, _dz, GravityPerSecond * Time.deltaTime );

            ms.Ani.SetFloat (Hash.dx,dx);
            ms.Ani.SetFloat (Hash.dz,dz);
        }

        public void WalkLateral ( Vector3 DirPerSecond )
        {
            if (on)
            {
                mccc.dir += Time.deltaTime * ground_movement.SlopeProjection (DirPerSecond, mgd.groundNormal);
                lateralDir += DirPerSecond;
            }
        }
    }
}
