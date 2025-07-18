using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorFaction : pix
    {
        public static ActorFaction o;
        public List<d_actor>[] factions { get; private set; } = new List<d_actor> [] { new List<d_actor>(), new List<d_actor>() };

        public override void Create()
        {
            o = this;
        }

        public static void Register (d_actor actor, int faction)
        {
            o.factions [faction].Add (actor);
        }

        public static List<d_actor> GetFoes ( int myfaction )
        {
            return o.factions [ myfaction == 1? 0 : 1 ];
        }
    }
}