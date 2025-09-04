using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lyra
{
    public static partial class Pile
    {
        public static readonly book <DotAuthor> Dot = new book<DotAuthor> ( "Dot" );
    }

    public sealed class book <T> where T : Object
    {
        private Dictionary <int, T> res;

        public book ( params string[] path )
        {
            LoadAll ( path );
        }

        void LoadAll ( string[] path )
        {
            res = new Dictionary<int, T> ();
            List<T> Ress = new List<T> ();

            foreach ( var p in path )
                Ress.AddRange ( Resources.LoadAll <T> ( p ) );

            foreach ( var r in Ress )
                res.Add ( new term ( r.name ), r );
        }

        public T[] radiate ()
        {
            return res.Values.ToArray ();
        }

        public T Q ( int id )
        {
            return res[id];
        }
    }
}