using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // TODO spectre linkage with sound effect // random sound effect
    public class Spectre : pix
    {
        public static Spectre o;

        Dictionary <int, ParticleSystem> Effects;

        public override void Create()
        {
            o = this;
            Effects = new Dictionary<int, ParticleSystem> ();
            var ParticleSystems = SubResources <ParticleSystem>.GetAll ();
            for (int i = 0; i < ParticleSystems.Length; i++)
            {
                var u = GameObject.Instantiate (ParticleSystems[i]);
                Effects.Add ( new term (ParticleSystems[i].name), u );
            }
        }

        public static void Fire ( int name, Vector3 pos )
        {
            ParticleSystem.EmitParams e = new ParticleSystem.EmitParams { position = pos };
            o.Effects[name].Emit (e, 1);
        }
    }
}