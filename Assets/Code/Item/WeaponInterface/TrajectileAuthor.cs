using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Arrow", menuName = "RPG/ArrowModel")]
    public class TrajectileAuthor : UnitAuthor
    {
        public PieceSkin Skin;

        [Header("Explosive module")]
        public string ExplosionEffect;
        public float ExplosionRadius;

        protected override void OnInstance(Unit newUnit)
        {
            newUnit.RequirePiece<p_trajectile>().Set(Skin);

            if ( !String.IsNullOrEmpty(ExplosionEffect) )
                newUnit.RequirePiece<p_t_explosive>().Set ( ExplosionRadius, new SuperKey (ExplosionEffect) );
        }
    }
}
