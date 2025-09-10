using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lyra
{
    public static class game_resources
    {
        public static readonly game_resources < VirtusCreator > virtus_so = new game_resources<VirtusCreator> ( "Virtus" );
        public static readonly game_resources < VirtusAuthor > virtus_go = new game_resources<VirtusAuthor> ("Spectre");
    }
    
    public sealed class game_resources <T> where T : Object
    {

        private Dictionary <int, T> _res;

        public game_resources ( params string[] path )
        {
            load ( path );
        }

        void load ( string[] path )
        {
            _res = new Dictionary<int, T> ();
            List<T> Ress = new List<T> ();

            foreach ( var p in path )
                Ress.AddRange ( Resources.LoadAll <T> ( p ) );

            foreach ( var r in Ress )
                _res.Add ( new term ( r.name ), r );
        }

        public T[] get_all ()
        {
            return _res.Values.ToArray ();
        }

        public T q ( int id )
        {
            return _res[id];
        }
    }
}