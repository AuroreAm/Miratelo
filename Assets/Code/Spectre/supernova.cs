/*using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class supernova : moon
    {
        static supernova o;
        static game_resources<ParticleSystem> res = new game_resources<ParticleSystem>("Spectre");
        Dictionary<int, dust.nebula> spectres;

        protected override void _ready()
        {
            o = this;

            spectres = new Dictionary<int, dust.nebula>();
            var systems = res.get_all();

            const string snv_ = "snv_";
            for (int i = 0; i < systems.Length; i++)
            {
                if (systems[i].gameObject.name.StartsWith(snv_))
                {
                    var u = GameObject.Instantiate(systems[i]);
                    spectres.Add(new term(systems[i].name), new dust.nebula(u, new term(systems[i].name)));
                }
            }
        }

        public static dust fire(int name, Vector3 pos)
        => o.spectres[name].fire(pos);

        public static void stop(dust handle)
        => o.spectres[handle.parent_name].stop(handle);
    }

    public sealed class dust
    {
        public int parent_name { private set; get; }
        public dust(int _parent_name)
        {
            parent_name = _parent_name;
        }

        public Vector3 _next_position;
        bool valid;

        public sealed class nebula : spectre
        {
            int ps_name;
            ParticleSystem ps;
            ParticleSystem.Particle[] particles;
            List<dust> dusts = new List<dust>();
            Stack<dust> pool = new Stack<dust>();

            protected override void __ready()
            {
                phoenix.core.start(this);
            }

            public nebula(ParticleSystem _ps, int _ps_name)
            {
                ps = _ps;
                ps_name = _ps_name;

                int max = ps.main.maxParticles;
                particles = new ParticleSystem.Particle[max];
            }

            public dust fire(Vector3 pos)
            {
                ParticleSystem.EmitParams e = new ParticleSystem.EmitParams { position = pos };
                ps.Emit(e, 1);
                return new_dust (pos);
            }

            public void stop ( dust dust )
            {
                dust.valid = false;
            }

            dust new_dust ( Vector3 pos )
            {
                dust h;
                if (pool.Count > 0)
                    h = pool.Pop();
                else h = new dust(ps_name);

                dusts.Add(h);

                h._next_position = pos;
                h.valid = true;
                return h;
            }

            protected override void _step()
            {
                ps.GetParticles(particles);

                for (int i = 0; i < dusts.Count; i++)
                {
                    if ( dusts[i].valid )
                    {
                        var previous = particles[i].position;
                        particles[i].position = dusts[i]._next_position;
                        particles[i].velocity = (particles[i].position - previous) / Time.deltaTime;
                    }
                    else
                    {
                        particles[i].position = new Vector3 (0,10000,0);
                        pool.Push ( dusts [i] );
                    }
                }

                ps.SetParticles(particles);
            }

        }
    }
}*/