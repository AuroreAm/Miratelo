using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorFaction : MonoBehaviour
    {
        public static ActorFaction o;
        public List<m_actor>[] factions { get; private set; } = new List<m_actor> [] { new List<m_actor>(), new List<m_actor>() };

        void Awake()
        {
            o = this;
        }

        public static void Register (m_actor actor, int faction)
        {
            o.factions [faction].Add (actor);
        }

        public static List<m_actor> GetFoes ( int myfaction )
        {
            return o.factions [ myfaction == 1? 0 : 1 ];
        }
    }
}