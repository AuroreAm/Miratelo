using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    // new ground movement complex
    // instead of the old using core, this one works with the new neuron system
    // can tweak between idle - walk - run - sprint - brake - brake rotation
    public class ac_ground_complex : motor
    {
        public override int Priority => Pri.def;
        public override bool AcceptSecondState => true;

        [Depend]
        s_capsule_character_controller sccc;
        int key_ccc;
        [Depend]
        s_gravity_ccc sgc;
        int key_gc;
        [Depend]
        s_skin ss;
        [Depend]
        d_ground_data dgd;
        [Depend]
        s_footstep sf; int key_f;

        public term state;
        /// <summary>
        /// composite walk direction for the character
        /// </summary>
        public Vector3 walkDir;
        /// <summary>
        /// direction the character is commanded to face
        /// </summary>
        public Vector3 rotDir;
        /// <summary>
        /// walk factor of the movement, used to interpolate between idle and walk, the character's speed is multiplied by this factor
        /// </summary>
        public float walkFactor;
        float sprintCooldown;

        int CurrentFrame;

        protected override void Start()
        {
            if ( CurrentFrame != Time.frameCount )
            {
                sprintCooldown = 0;
                walkDir = Vector3.zero;
                rotDir = Vecteur.LDir ( ss.rotY, Vector3.forward );
                ToIdle ();
            }
            // don't reset anything if this is aquired/freed on the same frame
            else
            {
                if (state == StateKey.idle)
                ToIdle ();
                else
                ToRun ();
            }

             key_ccc = Stage.Start ( sccc );
             key_gc = Stage.Start ( sgc );
             key_f = Stage.Start ( sf );
        }

        protected override void Step()
        {
            Animation ();
            Rotation ();
            SprintCooldown ();
            ResetDir ();
        }

        void ResetDir () => walkDir = Vector3.zero;

        protected override void Stop()
        {
            Stage.Stop (key_ccc);
            Stage.Stop (key_gc);
            Stage.Stop (key_f);

            ss.allowMoving = false;
            CurrentFrame = Time.frameCount;
        }

        void Animation()
        {
            // idle => run
            if (state == StateKey.idle && (walkDir.magnitude > 0.01f))
                {
                    ToRun();
                    Animation();
                    return;
                }
            // run modulation ( walk - run - sprint ) => idle
            else if (state == StateKey.run || state == StateKey.sprint || state == StateKey.walk)
            {
                if (!FactorCorrespondToState(walkFactor, state))
                ToRun();

                if (walkDir.magnitude < 0.01f)
                {
                    sf.StopFootStep ();
                    if (state == StateKey.sprint || sprintCooldown > 0 )
                    Brake ();
                    else
                    ToIdle ();
                }
            }
        }

        void ToIdle ()
        {
            ss.PlayState (0, AnimationKey.idle,0.1f);
            state = StateKey.idle;
        }

        void ToRun ()
        {
            if (state == StateKey.sprint)
                sprintCooldown = 0.5f;

            term Animation = (walkFactor == WalkFactor.walk) ? AnimationKey.walk : (walkFactor == WalkFactor.run) ? AnimationKey.run : AnimationKey.sprint;
            ss.PlayState (0, Animation ,0.2f);

            // get interval time from two footstep animation events from the clip
            sf.Play ( ss.EventPointsOfState ( Animation ) [1] - ss.EventPointsOfState ( Animation ) [0] );

            state =  (walkFactor == WalkFactor.walk) ? StateKey.walk : (walkFactor == WalkFactor.run) ? StateKey.run : StateKey.sprint;
        }

        void Brake ()
        {
            ss.PlayState (0, AnimationKey.sprint_brake,0.05f, BrakeEnd);
            state = StateKey.brake;
            ss.allowMoving = true;
            ss.SkinDir = ss.Ani.transform.forward.normalized;
        }

        void BrakeEnd ()
        {
            ss.allowMoving = false;
            ToIdle ();
        }

        static bool FactorCorrespondToState(float factor, term state)
        {
            return (factor == WalkFactor.walk && state == StateKey.walk) || (factor == WalkFactor.run && state == StateKey.run) || (factor == WalkFactor.sprint && state == StateKey.sprint);
        }

        void Rotation ()
        {
            float RotYTarget = 0;
            if (rotDir.magnitude > 0)
                RotYTarget =  Vecteur.RotDirectionY ( Vector3.zero, rotDir);

            // brake turn animation if rotation difference is too high // and is sprinting
            if (state == StateKey.sprint && Mathf.Abs(Mathf.DeltaAngle(ss.rotY.y, RotYTarget)) > 120)
                RotationBrake();

            if (state == StateKey.brake && Mathf.Abs(Mathf.DeltaAngle(ss.rotY.y, RotYTarget)) > 120)
                RotationBrake();

            ss.rotY = new Vector3(0, Mathf.MoveTowardsAngle(ss.rotY.y, RotYTarget, Time.deltaTime * 720), 0);
        }

        void RotationBrake()
        {
            ss.PlayState (0, AnimationKey.rotation_brake,0.05f, RotationBrakeEnd);
            state = StateKey.brake_rotation;
            ss.allowMoving = false;
        }

        void RotationBrakeEnd()
        {
            ToRun ();
        }

        void SprintCooldown ()
        {
            if (sprintCooldown > 0)
                sprintCooldown -= Time.deltaTime;
        }

        #region public methods

        public void Walk (Vector3 DirPerSecond, float WalkFactor = WalkFactor.run)
        {
            if (on)
            {
                walkDir += DirPerSecond;
                walkFactor = WalkFactor;

                if (walkDir.magnitude > 0.01f)
                rotDir = walkDir.normalized;

                if (state!=StateKey.brake_rotation && state != StateKey.idle)
                sccc.dir += Time.deltaTime * walkFactor * ground_movement.SlopeProjection (DirPerSecond, dgd.groundNormal);
            }
        }
        #endregion

    }

    
    public static class WalkFactor
    {
        public const float idle = 0;
        public const float walk = 0.15f;
        public const float tired = 0.14f;
        public const float run = 1;
        public const float sprint = 2;
    }
}