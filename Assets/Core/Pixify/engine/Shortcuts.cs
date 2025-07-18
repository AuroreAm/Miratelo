using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
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

}
