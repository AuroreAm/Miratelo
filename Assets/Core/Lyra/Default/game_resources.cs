using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lyra
{
    public static class game_resources
    {
        public static readonly game_resources < AudioClip > SE = new game_resources<AudioClip> ("SE");
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

    public sealed class res <T> {
        Dictionary < term, T > main = new Dictionary<term, T> ();

        public void add ( term key, T value ) {
            main.Add ( key, value );
        }

        public T q ( term key ) {
            return main[key];
        }
    }

    public static class resExtensions
    {
        public static T instantiate <T> (this res<T> r, term key) where T : Object {
            return Object.Instantiate ( r.q ( key ) );
        }
    }
}