using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorList : pix
    {
        static ActorList o;
        public List<d_actor>[] factions { get; private set; } = new List<d_actor> [] { new List<d_actor>(), new List<d_actor>() };
        Dictionary <string, d_actor> Actors = new Dictionary<string, d_actor> ();

        public override void Create()
        {
            o = this;
        }

        public static void Register (d_actor actor, int faction)
        {
            o.factions [faction].Add (actor);
            o.Actors.Add ( actor.ActorName, actor );
        }

        public static d_actor Get (string name)
        {
            return o.Actors [name];
        }

        public static List<d_actor> GetFoes ( int myfaction )
        {
            return o.factions [ myfaction == 1? 0 : 1 ];
        }
    }
}