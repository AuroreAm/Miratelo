using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    [CreateAssetMenu(fileName = "Arrow", menuName = "RPG/ArrowModel")]
    public class TrajectileAuthor : VirtusAuthor
    {
        public PieceSkin Skin;

        [Header("Explosive module")]
        public string ExplosionEffect;
        public float ExplosionRadius;

        protected override void RequiredPix(in List<Type> a)
        {
            a.A <a_trajectile> ();
            
            if ( !String.IsNullOrEmpty(ExplosionEffect) )
            a.A <a_t_explosive> ();
        }

        public override void OnWriteBlock()
        {
            new a_trajectile.package ( Skin );
            
            if ( !String.IsNullOrEmpty(ExplosionEffect) )
            new a_t_explosive.package ( ExplosionRadius, new term (ExplosionEffect) );
        }
    }
}
