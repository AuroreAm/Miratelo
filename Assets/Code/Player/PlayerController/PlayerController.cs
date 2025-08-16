using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;

namespace Triheroes.Code
{
    
    [Category("player controller")]
    public class player_cortex : cortex
    {
        [Depend]
        d_skill ds;

        public override void Think()
        {
            // basic movement
            AddReflexion <pr_move> ();
            AddReflexion <pr_jump> ();
            AddReflexion <r_fall_with_hard> ();

            // basic commands
            AddReflexion <pr_equip> ();
            AddReflexion <pr_target> ();
            AddReflexion <pr_interact_near_weapon> ();

            AddReflexion <pr_dash> ();
            AddReflexion <pr_sword> ();
            AddReflexion <pr_sword_target> ();
            AddReflexion <pr_aim> ();
        }
    }
}