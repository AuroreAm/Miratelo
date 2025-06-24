using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using static Pixify.treeBuilder;
using Triheroes.Code;

public class scr_enemy_test : script
{
    public override action CreateTree()
    {
        new selector () { fallback = true };
        
            new parallel () { Tag = StateKey2.zero };
                new sequence () { repeat = false };
                    new ac_set_adw_from_inv0 () { WeaponType = WeaponType.Sword, index = 0 };
                    new ac_draw_weapon ();
                    new ac_get_a_target () {Distance = 30};
                end ();

                new t_target ();
            end ();

            new sequence () { Tag = StateKey2.targetting, repeat = true };
                new ac_look_at_target ();
            end ();

        end ();

        return TreeFinalize ();
    }
}
