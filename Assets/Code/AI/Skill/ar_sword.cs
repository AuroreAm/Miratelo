using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class ar_sword : reflexion
    {
        
        [Depend]
        t_SS2 t_SS2;

        [Depend]
        ac_ss2 ac_SS2;

        protected override void Step()
        {
            if (t_SS2.on && !ac_SS2.on)
            Stage.Start ( ac_SS2 );
        }
    }

    public class ac_ss2 : action, IMotorHandler
    {
        [Depend]
        s_motor sm;
        motor [] Combo;

        [Depend]
        t_SS2 t_SS2;

        public void OnMotorEnd(motor m)
        {
            t_SS2.Finish ();
        }

        public override void Create()
        {
            Combo = new motor[3];

            for (int i = 0; i < 3; i++)
            {
                var motor_slash = new ac_slash ( SS2.SlashKeys [i] );
                b.IntegratePix (motor_slash);
                Combo[i] = motor_slash;
            }
        }

        protected override void Start()
        {
            var Success = sm.SetState ( Combo [ t_SS2.ComboId ], this );

            if (!Success)
            SelfStop ();
        }
    }


    [Category ("actor")]
    public class slash_condition : condition
    {
        [Depend]
        s_equip se;

        public override bool Check()
        {
            return se.weaponUser is s_sword_user;
        }

        public override term solution => Commands.draw_sword;
    }

}
