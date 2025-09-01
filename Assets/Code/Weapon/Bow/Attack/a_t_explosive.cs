using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // explosive module for trajectiles
    public class a_t_explosive : virtus.pixi
    {
        [Depend]
        a_trajectile host;

        float radius;
        int ExplosionEffect;

        public class package : PreBlock.Package <a_t_explosive>
        {
            public package ( float radius, int explosionEffect )
            {
                o.radius = radius;
                o.ExplosionEffect = explosionEffect;
            }
        }

        protected override void Stop()
        {
            Spectre.Fire ( ExplosionEffect, host.position );
            // TODO: send attack //
        }
    }
}
