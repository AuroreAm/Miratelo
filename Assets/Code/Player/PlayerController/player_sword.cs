using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lyra;
using Triheroes.Code.CapsuleAct;
using Triheroes.Code.Sword;
using Triheroes.Code.Sword.Combat;
using UnityEngine;

namespace Triheroes.Code
{
    [path("player controller")]
    public class player_sword : action, gold <hacked>
    {
        [link]
        SS1 SS1;

        public void _radiate(hacked gleam)
        {
            camera.o.hit_pause.spam ();
        }

        protected override void _step()
        {
            if ( player.action2.down )
            SS1.skill.spam ();
        }
    }

    [path("player controller")]
    public class player_sword_target : action2
    {
        [link]
        warrior warrior;
        [link]
        equip equip;
        [link]
        lock_target lock_target;
        [link]
        player_camera_target camera_target;

        protected override void _stepa()
        {
            if ( warrior.target && equip.weapon_user is sword_user )
            swap ();
        }

        protected override void _startb()
        {
            link ( lock_target );
            link ( camera_target );
        }

        protected override void _stepb()
        {
            if (!( warrior.target && equip.weapon_user is sword_user ))
            swap ();
        }

        protected override void _stopb()
        {
            unlink ( lock_target );
            unlink ( camera_target );
        }
    }

    [path("player controller")]
    public class player_parry : action
    {
        [link]
        slash_alert slash_alert;
        [link]
        arrow_alert arrow_alert;
        [link]
        equip equip;
        [link]
        motor motor;

        [link]
        SS9 SS9;

        Dictionary < term, act > acts_parry;

        protected override void _start()
        {
            link ( slash_alert );
            link ( arrow_alert );
        }

        protected override void _ready()
        {
            acts_parry = new Dictionary<term, act> ();

            var parry0 = new parry ( animation.SS8_0 );
            var parry1 = new parry ( animation.SS8_1 );

            acts_parry.Add ( animation.SS1_0, parry1 );
            acts_parry.Add ( animation.SS1_1, parry0 );
            acts_parry.Add ( animation.SS1_2, parry1 );

            acts_parry.Add ( animation.SS4, parry0 );
        }

        protected override void _step()
        {
            if ( equip.weapon_user is sword_user && player.action3.down )
            {
                if (arrow_alert.alert)
                {
                    if ( slash_alert.alert && slash_alert.timeleft < arrow_alert.timeleft )
                    {
                        parry_sword ();
                        return;
                    }

                    parry_arrow ();
                    return;
                }

                parry_sword ();
            }
        }

        void parry_sword ()
        {
            if ( acts_parry.ContainsKey ( slash_alert.incomming_slash.slash ) )
                motor.start_act ( acts_parry [ slash_alert.incomming_slash.slash ] );
            else
            motor.start_act ( acts_parry [ animation.SS1_0 ] );
        }

        void parry_arrow ()
        {
            SS9.skill.spam ();
        }
    }
}
