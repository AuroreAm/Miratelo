
using Lyra;
using Triheroes.Code.Inv0Act;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("player controller")]
    public class player_equip : action, act_handler, gold <_enter_interest>, gold <_exit_interest>
    {
        [link]
        motor motor;

        [link]
        equip equip;
        [link]
        inv0 inventory;

        [link]
        draw_weapon draw;

        [link]
        return_weapon @return;

        public void _act_end (act m, act_status status)
        {
            if ( status == act_status.done && m == @return && draw.prepared )
                motor.start_act2nd(draw, this);
        }

        protected override void _step()
        {
            if (player.W.down)
            {
                var freeSword = get_usable_sword();
                if (freeSword != -1 && equip.weapon_user == null)
                {
                    motor.start_act2nd( draw._(inventory.sword_place[freeSword]) ,  this);
                }
            }

            if (player.down.down)
            {
                var freeSword = get_usable_sword ();
                if (freeSword != -1 && equip.weapon_user != null)
                {
                    @return._(inventory.get_free_place_for(equip.weapon_user.weapon_base));
                    draw._(inventory.sword_place[freeSword]);

                    motor.start_act2nd (@return, this);
                }
            }

            if (player.aim.down && inventory.bow_place[0].occupied && equip.weapon_user == null)
                motor.start_act2nd(draw._(inventory.bow_place[0]), this);

            if (player.E.down && equip.weapon_user != null)
                motor.start_act2nd(@return._(inventory.get_free_place_for(equip.weapon_user.weapon_base)), this);

            if ( player.E.down && equip.weapon_user == null && current_interest != null ) {
                equip.link_weapon_user ( draw.get_corresponding_weapon_user ( current_interest )._ ( current_interest ) );
            }
        }

        int get_usable_sword ()
        {
            for (int i = 0; i < inventory.sword_place.Length; i++)
            {
                if (inventory.sword_place[i].occupied)
                    return i;
            }
            return -1;
        }
        
        weapon current_interest;
        public void _radiate(_exit_interest gleam) {
            if (gleam.interest is weapon_handle.i_interest_handler weapon && weapon.weapon == current_interest) {
                current_interest = null;
                ui.o.player_hud.prompt.end();
            }
        }
        public void _radiate(_enter_interest gleam) {
            if (gleam.interest is weapon_handle.i_interest_handler weapon) {
                current_interest = weapon.weapon;
                ui.o.player_hud.prompt.start(current_interest.GetType().Name);
            }
        }
    }
}