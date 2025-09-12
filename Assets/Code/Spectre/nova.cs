using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class nova : moon
    {
        static nova o;
        static game_resources <ParticleSystem> res = new game_resources<ParticleSystem> ( "Spectre" );
        Dictionary < int, ParticleSystem > spectres;

        protected override void _ready()
        {
            o = this;

            spectres = new Dictionary<int, ParticleSystem> ();
            var systems = res.get_all ();

            const string nv_ = "nv_";
            for (int i = 0; i < systems.Length; i++)
            {
                if (systems[i].gameObject.name.StartsWith (nv_))
                {
                    var u = GameObject.Instantiate (systems[i]);
                    spectres.Add ( new term (systems[i].name), u );
                }
            }
        }

        public static void fire ( int name, Vector3 pos )
        {
            ParticleSystem.EmitParams e = new ParticleSystem.EmitParams { position = pos };
            o.spectres[name].Emit (e, 1);
        }
    }
}