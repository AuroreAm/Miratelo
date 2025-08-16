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

        public bool SkillActive => (main != null && main.on);

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
}
