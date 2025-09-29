using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Lyra
{
    public static class DictionaryExtensions
	{
		public static void AddOrChange<T, T1>(this Dictionary<T, T1> d, T key, T1 value)
		{
			if (d.ContainsKey(key))
				d[key] = value;
			else
				d.Add(key, value);
		}

		// will return an new if the given key does not exisit
		public static T1 ForceGet<T, T1>(this Dictionary<T, T1> d, T key) where T1 : new()
		{
			if (d.ContainsKey(key))
				return d[key];
			else
			{
				var a = new T1();
				d.Add(key, a);
				return a;
			}
		}
	}

    public static class ListExtensions
    {
        public static void AddAtIndex <T> ( this List <T> l, int i, T element )
        {
            if ( i >= l.Count )
            l.Add (element);
            else
            l.Insert ( i, element );
        }

		public static void A <T> ( this List <Type> l )
		{
			l.Add ( typeof (T) );
		}
    }

	public static class Vector4Extensions
    {
        public static Vector2 xy(this Vector4 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector2 zw(this Vector4 v)
        {
            return new Vector2( v.z, v.w);
        }
    }

	public static class Dev
	{
		public static void Break ( string message )
		{
			UnityEngine.Debug.LogError ( message );
			System.Diagnostics.Debugger.Break ();
		}
	}

}