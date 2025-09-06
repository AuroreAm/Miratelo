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
            combo = new act [3];

            for (int i = 0; i < combo.Length; i++)
            combo [i] = new Sword.Combat.slash ( key [i] );
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
}
