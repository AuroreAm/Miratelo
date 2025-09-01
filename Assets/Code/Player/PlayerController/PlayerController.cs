using System.Collections;
using System.Collections.Generic;
using Lyra;
using Lyra.Spirit;
using UnityEditor.SceneManagement;

namespace Triheroes.Code
{
    
    [Category("player controller")]
    public class player_cortex : cortex
    {
        public override void Setup()
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
            AddReflexion <pr_SS2> ();
            AddReflexion <pr_parry> ();
            AddReflexion <pr_sword_target> ();
            AddReflexion <pr_aim> ();

            AddReflexion <r_slash_alert> ();
        }
    }
}