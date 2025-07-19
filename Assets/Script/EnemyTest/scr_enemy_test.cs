using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using Triheroes.Code;
using UnityEngine;

[Category ("ai")]
public class scr_enemy_test : cortex
{
    public override void Think()
    {
        AddReflexion < ar_move_way_point > ();
        AddReflexion < ar_way_to_target > (); 
        AddReflexion < ar_way_arround_target > (); 
        AddReflexion < ar_equip_equip_from_inv_0 > ();
        AddReflexion < ar_sword > ();
    }
}

[Category ("ai")]
public class reflexion_test : reflexion
{
    protected override void Step()
    {
        Debug.Log ("a");
    }
}