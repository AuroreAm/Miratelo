using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pixify
{
    public enum Comparator { less, more, equal, lessEqual, moreEqual }
    public static class Pixify
    {
        public static T CopyWithExport<T>(T a)
        {
            T b = (T)Activator.CreateInstance(a.GetType());
            foreach ( FieldInfo fi in a.GetType().GetFields( BindingFlags.Public | BindingFlags.Instance) )
            {
                if (fi.IsPublic && fi.GetCustomAttribute<ExportAttribute>() != null)
                    fi.SetValue(b, fi.GetValue(a));
            }
            return b;
        }

        public static T LoadIntoScene<T>(string path) where T : UnityEngine.Object
        {
            T o = Resources.Load<T>(path);

            if (!o)
            {
                Debug.LogError(string.Concat("hardcoded Resources not existing in ", path));
                return null;
            }
            else
                return ScriptableObject.Instantiate(o);
        }

        public static bool Compare(float A, float B, Comparator comparator)
        {
            switch (comparator)
            {
                case Comparator.less:
                    return A < B;
                case Comparator.more:
                    return A > B;
                case Comparator.equal:
                    return A == B;
                case Comparator.lessEqual:
                    return A <= B;
                case Comparator.moreEqual:
                    return A >= B;
            }
            return false;
        }
    }

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
}