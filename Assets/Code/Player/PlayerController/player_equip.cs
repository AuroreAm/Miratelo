using Lyra;
using Triheroes.Code.Inv0Act;

namespace Triheroes.Code
{
    [path ("player controller")]
    public class player_equip : action, acting
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

        public void _act_end (act m)
        {
            if (m == @return && draw.prepared)
                motor.start_act2nd(draw, this);
        }

        protected override void _step()
        {
            if (player.action2.down)
            {
                var freeSword = get_usable_sword();
                if (freeSword != -1 && equip.weapon_user == null)
                {
                    draw.set_place(inventory.sword_place[freeSword]);
                    motor.start_act2nd(draw, this);
                }
            }

            if (player.down.down)
            {
                var freeSword = get_usable_sword ();
                if (freeSword != -1 && equip.weapon_user != null)
                {
                    @return.set_place(inventory.get_free_place_for(equip.weapon_user.weapon_base));
                    draw.set_place(inventory.sword_place[freeSword]);

                    motor.start_act2nd(@return, this);
                }
            }

            if (player.aim.down && inventory.bow_place[0].occupied && equip.weapon_user == null)
            {
                draw.set_place(inventory.bow_place[0]);
                motor.start_act2nd(draw, this);
            }

            if (player.action1.down && equip.weapon_user != null)
            {
                @return.set_place(inventory.get_free_place_for(equip.weapon_user.weapon_base));
                motor.start_act2nd(@return, this);
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
    }
}