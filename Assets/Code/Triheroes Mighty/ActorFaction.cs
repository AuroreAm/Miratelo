using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorFaction : MonoBehaviour
    {
        public static ActorFaction o;
        public Dictionary<int, List<m_actor>> factions { get; private set; } = new Dictionary<int, List<m_actor>>();

        void Awake()
        {
            o = this;
        }

        public static void Register (m_actor actor, int faction)
        {
            if (!o.factions.ContainsKey (faction))
                o.factions.Add (faction, new List<m_actor> ());
            o.factions [faction].Add (actor);
        }
    }
}