using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public static class SubResources <T> where T : Object
    {
        static Dictionary <int, T> Res;
        public static void LoadAll ( string path )
        {
            Res = new Dictionary<int, T> ();
            T[] Ress = Resources.LoadAll <T> ( path );

            foreach ( var r in Ress )
                Res.Add ( new SuperKey ( r.name), r );
        }

        public static T q ( int id )
        {
            return Res[id];
        }
    }
}