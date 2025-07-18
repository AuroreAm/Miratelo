using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
namespace Pixify.Spirit
{
    public abstract class task : pixi.self_managed
    {
    }

    public class motor_main_task : task, IMotorHandler
    {
        [Depend]
        s_motor sm;
        motor main;

        public motor_main_task (motor motor)
        {
            main = motor;
        }

        public override void Create()
        {
            b.IntegratePix ( main );
        }

        protected override void Start()
        {
            var Success = sm.SetState (main, this);

            if (!Success)
            SelfStop ();
        }

        public void OnMotorEnd(motor m)
        {
            if (on)
            SelfStop ();
        }
    }

    public class motor_second_task : task, IMotorHandler
    {
        [Depend]
        s_motor sm;
        motor main;

        public motor_second_task (motor motor)
        {
            main = motor;
        }

        public override void Create()
        {
            b.IntegratePix ( main );
        }

        protected override void Start()
        {
            var Success = sm.SetSecondState (main, this);

            if (!Success)
            SelfStop ();
        }

        public void OnMotorEnd(motor m)
        {
            if (on)
            SelfStop ();
        }
    }
}*/