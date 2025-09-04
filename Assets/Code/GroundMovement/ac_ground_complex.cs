using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // new ground movement complex
    // instead of the old using core, this one works with the new neuron system
    // can tweak between idle - walk - run - sprint - brake - brake rotation
    public class ac_ground_complex : motor
    {
        public override int Priority => Rank.Def2nd;
        public override bool AcceptSecondState => true;

        [Link]
        s_capsule_character_controller capsule;
        [Link]
        s_gravity_ccc gravitySys;
        [Link]
        s_skin skin;
        [Link]
        d_ground ground;
        [Link]
        d_ground_data groundData;
        [Link]
        s_footstep footstep;

        public term state { private set; get; }

        /// <summary>
        /// composite walk direction
        /// </summary>
        Vector3 walkDir;

        /// <summary>
        /// walk factor of the movement, used to interpolate between idle and walk, the character's speed is multiplied by this factor
        /// </summary>
        public float WalkFactor;

        float sprintCooldown;
        int currentFrame;

        protected override void OnStart()
        {
            this.Link (capsule);
            this.Link (gravitySys);
            this.Link (footstep);
            
            ground.Use (this);

            if ( currentFrame != Time.frameCount )
            {
                sprintCooldown = 0;
                walkDir = Vector3.zero;
                ToIdle ();
            }
            // don't reset anything if this is aquired/freed on the same frame
            else
            {
                if (state == idle)
                ToIdle ();
                else
                ToRun ();
            }
        }

        protected override void OnStep()
        {
            Animation ();
            Rotation ();
            SprintCooldown ();
            ResetDir ();
        }

        void ResetDir () => walkDir = Vector3.zero;

        protected override void OnStop()
        {
            skin.RootOfCharacterTransform = false;
            currentFrame = Time.frameCount;
        }

        void Animation()
        {
            // idle => run
            if (state == idle && (walkDir.magnitude > 0.01f))
                {
                    ToRun();
                    Animation();
                    return;
                }
            // run modulation ( walk - run - sprint ) => idle
            else if (state == run || state == sprint || state == walk)
            {
                if (!FactorCorrespondToState(WalkFactor, state))
                ToRun();

                if (walkDir.magnitude < 0.01f)
                {
                    // INPROGRESS
                    // footstep.StopFootStep ();
                    if (state == sprint || sprintCooldown > 0 )
                    Brake ();
                    else
                    ToIdle ();
                }
            }
        }

        void ToIdle ()
        {
            skin.PlayState ( new SkinAnimation ( idle, this ) );
            state = idle;
        }

        void ToRun ()
        {
            if (state == sprint)
                sprintCooldown = 0.5f;

            term Animation = (WalkFactor == Code.WalkFactor.walk) ? walk : (WalkFactor == Code.WalkFactor.run) ? run : sprint;
            skin.PlayState ( new SkinAnimation (Animation, this) { Fade = .2f } );

            // get interval time from two footstep animation events from the clip
            // footstep.Play ( skin.EventPointsOfState ( Animation ) [1] - skin.EventPointsOfState ( Animation ) [0] );

            state =  (WalkFactor == Code.WalkFactor.walk) ? walk : (WalkFactor == Code.WalkFactor.run) ? run : sprint;
        }

        void Brake ()
        {
            skin.PlayState ( new SkinAnimation ( brake, this ) { End = BrakeEnd, Fade = .05f } );
            state = brake;
            skin.RootOfCharacterTransform = true;
            skin.SkinDir = skin.Ani.transform.forward.normalized;
        }

        void BrakeEnd ()
        {
            skin.RootOfCharacterTransform = false;
            ToIdle ();
        }

        static bool FactorCorrespondToState(float factor, term state)
        {
            return (factor == Code.WalkFactor.walk && state == walk) || (factor == Code.WalkFactor.run && state == run) || (factor == Code.WalkFactor.sprint && state == sprint);
        }

        void Rotation ()
        {
            if (walkDir.magnitude > 0)
                ground.RotY =  Vecteur.RotDirectionY ( Vector3.zero, walkDir);

            // brake turn animation if rotation difference is too high // and is sprinting
            if (state == sprint && Mathf.Abs(Mathf.DeltaAngle(ground.RotY, skin.RotY)) > 120)
                RotationBrake();

            if (state == brake && Mathf.Abs(Mathf.DeltaAngle(ground.RotY, skin.RotY)) > 120)
                RotationBrake();

            ground.PerformRotation ();
        }

        void RotationBrake()
        {
            skin.PlayState ( new SkinAnimation ( rotation_brake, this ) { End = RotationBrakeEnd, Fade = .05f } );
            state = rotation_brake;
            skin.RootOfCharacterTransform = false;
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
        public void Walk (Vector3 DirPerSecond, float WalkFactor = Code.WalkFactor.run)
        {
            if (on)
            {
                walkDir += DirPerSecond;
                this.WalkFactor = WalkFactor;

                if (state!=rotation_brake && state != idle)
                    capsule.Dir += Time.deltaTime * this.WalkFactor * d_ground.SlopeProjection (DirPerSecond, groundData.groundNormal);
            }
        }
        #endregion

        public static readonly term idle = new term ("idle");
        public static readonly term sprint = new term ("sprint");
        public static readonly term brake = new term ("brake");
        public static readonly term rotation_brake = new term ("rotation_brake");
        public static readonly term walk = new term ("walk");
        public static readonly term run = new term ("run");
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