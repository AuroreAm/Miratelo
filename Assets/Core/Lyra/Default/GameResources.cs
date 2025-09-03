using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lyra
{
    public static partial class GameResources
    {
        public static readonly SubResources <VirtusAuthor> Virtus = new SubResources<VirtusAuthor> ( "Virtus" );
    }

    public sealed class SubResources <T> where T : Object
    {
        private Dictionary <int, T> _res;

        public SubResources ( params string[] path )
        {
            LoadAll ( path );
        }

        void LoadAll ( string[] path )
        {
            _res = new Dictionary<int, T> ();
            List<T> Ress = new List<T> ();

            foreach ( var p in path )
                Ress.AddRange ( Resources.LoadAll <T> ( p ) );

            foreach ( var r in Ress )
                _res.Add ( new term ( r.name ), r );
        }

        public T[] GetAll ()
        {
            return _res.Values.ToArray ();
        }

        public T Q ( int id )
        {
            return _res[id];
        }
    }
}