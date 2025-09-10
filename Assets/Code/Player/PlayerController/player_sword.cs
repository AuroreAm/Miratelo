using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code.Sword.Combat;
using UnityEngine;

namespace Triheroes.Code
{
    [path("player controller")]
    public class player_sword : action, acting
    {
        public act[] combo;
        static readonly term[] key = { animation.SS1_0, animation.SS1_1, animation.SS1_2 };
        
        int ptr;
        bool ready_for_next;

        [link]
        motor motor;
        [link]
        equip equip;

        act current;

        protected override void _ready()
        {
            combo = new act [6];

            for (int i = 0; i < 3; i++)
            combo [i] = new slay ( key [i] );

            combo [3] = new slay_hook_up ( key [0] );
            combo [4] = new slay_hook_spam ( key [1] );
            combo [5] = new slay_hook_spam ( key [2] );
        }

        protected override void _step()
        {
            if ( player.action2.down && equip.weapon_user is sword_user )
                spam ();
        }

        public void _act_end(act m)
        {
            if ( ready_for_next )
            ptr ++;
            else
            {
                ptr = 0;
                return;
            }

            ready_for_next = false;
            if (ptr < combo.Length)
            motor.start_act ( combo [ptr], this );
            else
            ptr = 0;
        }

        bool active => current != null && current.on;

        public void spam ()
        {
            if ( !active )
            start_act ( combo [ptr] );
            else if ( !ready_for_next )
            ready_for_next = true;
        }

        void start_act ( act act )
        {
            current = act;
            motor.start_act ( act, this );
        }
    }

    [path("player controller")]
    public class player_parry : action, acting
    {
        [link]
        slash_alert slash_alert;
        [link]
        arrow_alert arrow_alert;
        [link]
        equip equip;
        [link]
        motor motor;

        Dictionary < term, act > acts_parry;
        act act_parry_arrow;

        public void _act_end(act m)
        {}

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

            acts_parry.Add ( animation.SS4, parry1 );

            act_parry_arrow = new parry_arrow ();
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
                motor.start_act ( acts_parry [ slash_alert.incomming_slash.slash ] , this );
        }

        void parry_arrow ()
        {
            motor.start_act ( act_parry_arrow, this );
        }
    }
}
