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
                    new ac_equip_adw (0);
                    new ac_draw_weapon ();
                    new ac_get_a_target () {Distance = 30};
                end ();

                new t_target ();
            end ();

            new sequence () { Tag = StateKey2.targetting, repeat = true };
                new ac_walk_to_target () {Speed = 5, StopDistance = 5, StopWhenDone = true};
                new ac_walk_arround_target () { AngleAmount = 90, Speed = 5 };
                new ac_look_at_target ();
            end ();

        end ();

        return TreeFinalize ();
    }
}
