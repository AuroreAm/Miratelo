using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Triheroes.Code
{
    [need_ready]
    public class slay_combo : action, act_handler
    {
        [link]
        stamina stamina;

        act [] combo;

        int ptr;
        bool ready_for_next;

        [link]
        motor motor;
        [link]
        equip equip;

        public act this [int i] => combo [i];

        public slay_combo ( act [] _combo )
        {
            combo = _combo;
        }

        public void spam ()
        {
            if ( !can_skill () ) return;

            if ( !on )
            {
                ready_for_tick ();
                phoenix.core.start_action (this);
            }
            else
            ready_for_next = true;
        }

        bool can_skill ()
        {
            return equip.weapon_user is sword_user;
        }

        protected override void _start()
        {
            ready_for_next = false;
            ptr = 0;
            start_motor ();
        }

        protected override void _step()
        {
            if ( motor.act != combo [ptr] )
            stop ();
        }

        public void _act_end(act a, act_status status) {
            if ( status != act_status.done )
            return;

            if ( ready_for_next )
            ptr ++;
            else
            {
                ptr = 0;
                return;
            }

            ready_for_next = false;
            if (ptr < combo.Length)
            start_motor ();
            else
            ptr = 0;
        }

        void start_motor () {
            bool success = motor.start_act ( combo [ptr], this );
            if (success)
            stamina.use ( 1 );
        }
    }
}
