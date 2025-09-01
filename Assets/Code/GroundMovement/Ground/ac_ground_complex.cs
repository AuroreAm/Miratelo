using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    // new ground movement complex
    // instead of the old using core, this one works with the new neuron system
    // can tweak between idle - walk - run - sprint - brake - brake rotation
    public class ac_ground_complex : motor
    {
        public override int Priority => Pri.def2nd;
        public override bool AcceptSecondState => true;

        [Depend]
        s_capsule_character_controller sccc;
        [Depend]
        s_gravity_ccc sgc;
        [Depend]
        s_skin ss;
        [Depend]
        d_ground dg;
        [Depend]
        d_ground_data dgd;
        [Depend]
        s_footstep sf;

        public term state { private set; get; }
        /// <summary>
        /// composite walk direction for the character
        /// </summary>
        Vector3 walkDir;
        /// <summary>
        /// walk factor of the movement, used to interpolate between idle and walk, the character's speed is multiplied by this factor
        /// </summary>
        public float walkFactor;
        float sprintCooldown;

        int CurrentFrame;

        public override void Create()
        {
            Link (sccc);
            Link (sgc);
            Link (sf);
        }

        protected override void Start()
        {
            dg.use (this);

            if ( CurrentFrame != Time.frameCount )
            {
                sprintCooldown = 0;
                walkDir = Vector3.zero;
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
            if (walkDir.magnitude > 0)
                dg.rotY =  Vecteur.RotDirectionY ( Vector3.zero, walkDir);

            // brake turn animation if rotation difference is too high // and is sprinting
            if (state == StateKey.sprint && Mathf.Abs(Mathf.DeltaAngle(ss.rotY, dg.rotY)) > 120)
                RotationBrake();

            if (state == StateKey.brake && Mathf.Abs(Mathf.DeltaAngle(ss.rotY, dg.rotY)) > 120)
                RotationBrake();

            dg.PerformRotation ();
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

                if (state!=StateKey.brake_rotation && state != StateKey.idle)
                sccc.dir += Time.deltaTime * walkFactor * d_ground.SlopeProjection (DirPerSecond, dgd.groundNormal);
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