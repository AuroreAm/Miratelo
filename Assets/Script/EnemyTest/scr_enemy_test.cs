using System.Collections;
using System.Collections.Generic;
using Pixify;
using Triheroes.Code;
using UnityEngine;

[Category ("Test")]
public class scr_enemy_test : cortex
{
    [Depend]
    m_skill ms;

    protected override void Think()
    {
        AddReflection <r_idle> ();

        if ( ms.SkillValid <SS2_consecutive> () )
        AddReflection <ai_slash_consecutive> ();
    }
}