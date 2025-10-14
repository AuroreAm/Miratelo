using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class xenos : index < hitbox > {
        static xenos o;

        #region warrior
        public List<warrior>[] factions { get; private set; } = new List<warrior> [] { new List<warrior>(), new List<warrior>() };
        Dictionary <term, warrior> warriors = new Dictionary<term, warrior> ();

        public static bool contains ( term name )
        {
            return o.warriors.ContainsKey (name);
        }

        public static actor get_actor ( term name )
        {
            return (actor) o.warriors [name];
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
        #endregion

        public xenos () {
            o = this;
        }

        public static void damage ( int collider_id, damage damage ) {
            o.ptr [collider_id].damage ( damage );
        }

        public static bool is_enemy ( int collider_id, int self_faction ) {
            if ( !o.ptr.ContainsKey (collider_id) ) return false;

            if ( o.ptr[collider_id].warrior.faction != self_faction )
            return true;

            return false;
        }

        public static int character_id_of ( int collider_id ) {
            return o.ptr [ collider_id ].character_id;
        }

        public static photon photon_of ( int collider_id ) {
            return o.ptr [collider_id].warrior.photon;
        }

        static List <photon> photons = new List<photon> ();
        /// <returns> all enemy photon, this list is temporary and repopulated each call </returns>
        public static List <photon> enemy_of ( Collider [] pool, int self_faction ) {
            photons.Clear ();
            foreach ( var c in pool ) {
                if ( contains ( c.uid () ) && ! photons.Contains ( o.ptr [ c.uid () ].warrior.photon ) && is_enemy ( c.uid (), self_faction ) ) {
                    photons.Add ( o.ptr [ c.uid () ].warrior.photon );
                }
            }
            return photons;
        }

        /// <returns> all enemy photon, this list is temporary and repopulated each call </returns>
        public static List <photon> enemy_of ( RaycastHit [] pool, int self_faction ) {
            photons.Clear ();
            foreach ( var h in pool ) {
                if ( contains ( h.collider.uid () ) && ! photons.Contains ( o.ptr [ h.collider.uid () ].warrior.photon ) && is_enemy ( h.collider.uid (), self_faction ) ) {
                    photons.Add ( o.ptr [ h.collider.uid () ].warrior.photon );
                }
            }
            return photons;
        }
        
    }
}
