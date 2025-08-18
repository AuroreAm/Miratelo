using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    public abstract class skill_motor : skill_box, IMotorHandler
    {
        public bool on => true;

        [Depend]
        s_motor sm;
        motor main;

        skill_motor ()
        {}

        public bool Active => (main != null && main.on);

        public abstract class First : skill_motor
        {
            protected bool StartMotor (motor motor)
            {
                main = motor;
                return sm.SetState ( motor, this );
            }

            protected void StopMotor ()
            {
                if (main.on)
                sm.EndState ( this );
            }
        }

        public abstract class Second : skill_motor
        {
            protected bool StartMotor2nd (motor motor)
            {
                main = motor;
                return sm.SetSecondState ( motor, this );
            }

            protected void StopMotor2nd ()
            {
                if (main.on)
                sm.EndSecondState ( this );
            }
        }

        public virtual void OnMotorEnd(motor m)
        {}
    }

    public abstract class skill_reflexion <T> : reflexion where T: skill_box
    {
        [Depend]
        d_skill ds;

        protected T skill => ds.GetSkill <T> ();

        // TODO: getting skill each frame is expensive, find a better way
        protected sealed override void Reflex()
        {
            if ( ds.SkillValid <T> () )
            SkillReflex ( ds.GetSkill <T> () );
        }

        /// <summary>
        /// called when skill is valid
        /// </summary>
        protected virtual void SkillReflex ( T skill )
        {}
    }
}
