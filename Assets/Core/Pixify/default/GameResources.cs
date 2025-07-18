using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixify
{
    public static class SubResources <T> where T : Object
    {
        static Dictionary <int, T> Res;
        public static void LoadAll ( params string[] path )
        {
            Res = new Dictionary<int, T> ();
            List<T> Ress = new List<T> ();

            foreach ( var p in path )
                Ress.AddRange ( Resources.LoadAll <T> ( p ) );

            foreach ( var r in Ress )
                Res.Add ( new term ( r.name ), r );
        }

        public static T[] GetAll ()
        {
            return Res.Values.ToArray ();
        }

        public static T q ( int id )
        {
            return Res[id];
        }
    }
}