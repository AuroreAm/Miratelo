using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // explosive module for trajectiles
    public class p_t_explosive : piece
    {
        [Depend]
        p_trajectile host;

        float radius;
        int ExplosionEffect;


        public void Set ( float radius, int explosionEffect )
        {
            this.radius = radius;
            ExplosionEffect = explosionEffect;
        }

        public override void Main()
        {}

        protected override void OnFree()
        {
            Spectre.Fire ( ExplosionEffect, host.position );
            // TODO: send attack //
        }
    }
}
