using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

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
            /*
            sm.AddReflexion <pr_target> ();
            sm.AddReflexion <pr_interact_near_weapon> ();

            // dash skill
            if ( ds.SkillValid <DS0_dash> () )
            sm.AddReflexion <pr_dash> (); */

            if ( ds.SkillValid <SS2_consecutive> ())
            AddReflexion <pr_slash_consecutive> ();
        }
    }
}