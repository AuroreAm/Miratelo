using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class pallas : index <element>
    {
        static pallas o;
        public List<warrior>[] factions { get; private set; } = new List<warrior> [] { new List<warrior>(), new List<warrior>() };
        Dictionary <term, warrior> warriors = new Dictionary<term, warrior> ();

        public pallas ()
        {
            o = this;
        }

        public static void register (warrior warrior, int faction)
        {
            o.factions [faction].Add (warrior);
            o.warriors.Add ( new term ( ((actor) warrior).name ), warrior );
        }

        public static List < warrior > get_foes ( int myfaction )
        {
            return o.factions [ myfaction == 1? 0 : 1 ];
        }

        public static bool is_enemy ( int id, int self )
        {
            if ( !o.ptr.ContainsKey (id) ) return false;

            if ( o.ptr[id].warrior.faction != self )
            return true;

            return false;
        }

        public static void radiate <T> ( int to, T gleam ) where T : struct
        {
            o.ptr [to].photon.radiate (gleam);
        }
    }
}