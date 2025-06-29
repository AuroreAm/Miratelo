using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class Character : MonoBehaviour
    {
        // library of unique module indexed by type
        // polymorphism not supported
        protected Dictionary < Type, module > modules;

        // library of action unique for the character but scatered in graph
        public Dictionary < Type, action > uniques { private set; get; } = new Dictionary<Type, action> ();

        public T GetUnique <T> () where T : action
        {
            if (uniques.TryGetValue(typeof(T), out action n))
                return n as T;
            else
            throw new InvalidOperationException("cannot find unique action of type " + typeof(T));
        }


        List < node > nodes;
        uint ptr;

        void Awake ()
        {
            modules = new Dictionary<Type, module> ();
            nodes = new List<node> ();
        }

        node RequireNode (Type nodeType)
        {
            if (nodeType.IsSubclassOf(typeof (action)))
            {
                if (treeBuilder.Initialized)
                Debug.LogWarning ("node with action dependance detected in Treebuilder, avoid this");
                return RequireUnique (nodeType);
            }
            else if (nodeType.IsSubclassOf(typeof (module)))
                return RequireModule (nodeType);
            else
                throw new InvalidOperationException("cannot depend on a non action or module type");
        }
        action RequireUnique (Type actiontype)
        {
            if (!actiontype.IsSubclassOf(typeof (action)))
                throw new InvalidOperationException("cannot depend on a non action type");

            if (uniques.TryGetValue(actiontype, out action a))
                return a;
            else
            {
                a = Activator.CreateInstance(actiontype) as action;
                uniques.Add(actiontype, a);

                RegisterNode (a);
                a.Create ();

                return a;
            }
        }
        module RequireModule (Type moduleType)
        {
            if (!moduleType.IsSubclassOf(typeof (module)))
                throw new InvalidOperationException("cannot depend on a non module type");

            if (modules.TryGetValue(moduleType, out module m))
                return m;
            else
            {
                m = Activator.CreateInstance(moduleType) as module;
                modules.Add(moduleType, m);

                m.character = this;
                RegisterNode (m);
                m.Create ();

                return m;
            }
        }

        /// <summary>
        /// return a module of the type requested
        /// if the module is not found, it will be created
        /// does not work with constructor arguments
        /// </summary>
        public T RequireModule <T> () where T : module, new ()
        {
            if (modules.TryGetValue(typeof(T), out module m))
                return m as T;
            else
            {
                var n = new T();
                modules.Add(typeof(T), n);

                n.character = this;
                RegisterNode (n);
                n.Create ();

                return n;
            }
        }

        public T GetModule <T> () where T : module
        {
             if (modules.TryGetValue(typeof(T), out module m))
                return m as T;
            else
                return null;
        }

        void RegisterNode ( node m )
        {
            ptr ++;
            m.nodeId = ptr;
            nodes.Add (m);

            Type current = m.GetType ();
            while ( current != typeof ( node ) )
            {
                var fis = current.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var fi in fis)
                {
                if (fi.GetCustomAttribute<DependAttribute>() != null)
                    fi.SetValue ( m, RequireNode(fi.FieldType) );
                }
                current = current.BaseType;
            }
        }

        /// <summary>
        /// connect and initialize this node with the character if it is not connected
        /// </summary>
        public node ConnectNode ( node n )
        {
            if (!nodes.Contains (n))
            {
                RegisterNode (n);
                n.Create ();
            }
            return n;
        }

        public T ConnectNode <T> (T a) where T:node
        {
            if (!nodes.Contains (a))
            {
                RegisterNode (a);
                a.Create ();
            }

            // NOTE: when a module is connected here, it has no character attached to

            return a;
        }
    }
}