using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_blackboard : module
    {
        public blackboard bb {get;private set;} = new blackboard ();
    }

    public class blackboard
    {
        private Dictionary <int, object> data = new Dictionary<int, object> ();

        public bool Exist ( int key ) => data.ContainsKey ( key );

        public T get<T> (int key)
        {
            return (T)data [key];
        }
        
        public void Set ( int key, object value )
        {
            if (data.ContainsKey (key))
                data [key] = value;
            else
                data.Add (key, value);
        }

        public bool Compare ( int key, object expected )
        {
            if (data.ContainsKey (key))
                return data [key].Equals (expected);
            else
                return false;
        }

        public void CloneTo ( blackboard other )
        {
            other.data.Clear ();
            foreach (KeyValuePair<int, object> entry in data)
                other.data.Add (entry.Key, entry.Value);
        }
    }
}