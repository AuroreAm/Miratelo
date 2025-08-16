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

[Category ("script")]
public class scr_enemy_test_while_target_aprroach_rapidly_reaction : reflexion_flow
{
    [Depend]
    d_actor da;
    [Depend]
    ar_move_way_point amwp;

    float DistanceToTarget;

    protected override void Start()
    {
        if (!da.target) return;

        DistanceToTarget = Vector3.Distance ( da.dd.position, da.target.dd.position );
    }

    protected override void Step()
    {
        if (!da.target) return;

        if ( Vector3.Distance ( da.dd.position, da.target.dd.position ) < DistanceToTarget - amwp.LastDir.magnitude * 2 )
            {}

        DistanceToTarget = Vector3.Distance ( da.dd.position, da.target.dd.position );
    }
}