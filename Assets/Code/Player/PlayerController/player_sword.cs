using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path("player controller")]
    public class player_sword : action, gold <hacked>
    {
        [link]
        skills s;

        public void _radiate(hacked gleam)
        {
            camera.o.hit_pause.spam ();
        }

        protected override void _step()
        {
            if ( player.W.down )
            s.get<SS1> ().spam ();
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
        tps_target camera_target;

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
        skills s;

        Dictionary < term, act > acts_parry;

        protected override void _start()
        {
            link ( slash_alert );
            link ( arrow_alert );
        }

        protected override void _ready()
        {
            acts_parry = new Dictionary<term, act> ();

            var parry0 = with ( new parry ( anim.SS8_0 ) );
            var parry1 = with (new parry ( anim.SS8_1 ) );

            acts_parry.Add ( anim.SS1_0, parry1 );
            acts_parry.Add ( anim.SS1_1, parry0 );
            acts_parry.Add ( anim.SS1_2, parry1 );

            acts_parry.Add ( anim.SS4, parry1 );
        }

        protected override void _step()
        {
            if ( equip.weapon_user is sword_user && player.N.down )
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
                motor.start ( acts_parry [ slash_alert.incomming_slash.slash ] );
            else
            motor.start ( acts_parry [ anim.SS1_0 ] );
        }

        void parry_arrow ()
        {
            s.get <SS9> ().spam ();
        }
    }
}
