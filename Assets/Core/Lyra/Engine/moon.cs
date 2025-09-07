using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    [path("")]
    public abstract class moon
    {
        static uint static_counter = 0;
        public uint id { get; private set; }
        private bool ready;
        public system system { get; private set; }

        public moon ()
        {
            #if UNITY_EDITOR
            if ( ! UnityEditor.EditorApplication.isPlaying )
            return;
            #endif

            id = ++static_counter;

            if ( system_domain.Count > 0 && system_domain.Peek() != null )
                system_domain.Peek().add(this);
        }

        internal static Stack <system> system_domain = new Stack<system> ();

        protected virtual void _ready ()
        { }

        internal void set_orbit ( system structure )
        {
            if (GetType().GetCustomAttribute<inked>() != null && !ready)
                Debug.LogError($"{GetType().Name} was structured without package");

            system = structure;
            enter_my_system_field();
            _ready ();
            exit_my_system_field();
        }

        protected void enter_my_system_field ()
        {
            system_domain.Push ( system );
        }

        protected void exit_my_system_field ()
        {
            system_domain.Pop ();
        }

        public abstract class ink <T> where T : moon, new()
        {
            public ink()
            {
                if (system.creator.o == null)
                    throw new InvalidOperationException("package can only be instantied in OnStructure ()");

                o = system.creator.querry<T>();
                o.ready = true;
            }
            public T o { private set; get; }
        }
    }

    public sealed class ink<T> where T : moon, new()
    {
        public ink ()
        {
            if (system.creator.o == null)
                throw new InvalidOperationException("Querry can only be called in OnStructure ()");

            o = system.creator.querry<T>();
        }

        public T o { private set; get; }
    }

    public class system
    {
        Dictionary<Type, moon> planets = new Dictionary<Type, moon>();
        List<moon> satellites = new List<moon>();

        private system( Dictionary<Type, moon> founders )
        {
            satellites.AddRange(founders.Values);

            foreach (var kvp in founders)
                planets.Add(kvp.Key, kvp.Value);

            var founderBrick = satellites.ToArray();
            foreach (var d in founderBrick)
                link_dependency(d);

            foreach (var d in founderBrick)
                d.set_orbit(this);

            foreach (var d in founderBrick)
                new_member(d);
        }

        public void add ( moon item )
        {
            if (satellites.Contains (item) )
                return;

            satellites.Add ( item );
            if (!planets.ContainsKey(item.GetType()))
                planets.Add(item.GetType(), item);

            link_dependency(item);

            item.set_orbit(this);
            new_member (item);
        }

        void link_dependency ( moon item )
        {
            Type current = item.GetType();
            while (current != typeof(moon))
            {
                var fis = current.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var fi in fis)
                {
                    if (fi.GetCustomAttribute<linkAttribute>() != null)
                        fi.SetValue(item, get_or_add ( fi.FieldType ) );
                }
                current = current.BaseType;
            }
        }

        moon get_or_add ( Type t )
        {
            if (planets.ContainsKey(t))
                return planets[t];
            else
            {
                var c = (moon)Activator.CreateInstance(t);
                add(c);
                return c;
            }
        }

        public T get <T>() where T : moon
        {
            if (planets.TryGetValue(typeof(T), out moon m))
                return m as T;
            else
                return null;
        }

        // local callback
        event Action<moon> new_member = delegate { };
        public event Action<moon> _new_member
        {
            add { new_member += value; }
            remove { new_member -= value; }
        }

        public class creator
        {
            internal static creator o => stack.Peek ();
            static Stack < creator > stack = new Stack<creator> ();

            Lyra.creator author;
            Dictionary< Type, moon > founder = new Dictionary < Type, moon >();

            public creator (Lyra.creator _author)
            {
                author = _author;
            }

            public creator (Type[] types, Lyra.creator _author )
            {
                author = _author;

                foreach (var x in types)
                    get_or_add_founder(x);
            }

            static internal T querry <T>() where T : moon => o.get_or_add_founder(typeof(T)) as T;

            moon get_or_add_founder (Type type)
            {
                if ( founder.ContainsKey ( type ) )
                    return founder [type];
                else
                {
                    var c = Activator.CreateInstance(type) as moon;
                    founder.Add ( type, c );
                    check_dependency ( type );
                    return c;
                }
            }

            void check_dependency ( Type type )
            {
                Type current = type;

                if ( !type.IsSubclassOf(typeof(moon)) )
                    throw new InvalidOperationException("only moon type can be in system");

                while ( current != typeof(moon) )
                {
                    var fis = current.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    foreach (var fi in fis)
                    {
                        if (fi.GetCustomAttribute<linkAttribute>() != null)
                            get_or_add_founder(fi.FieldType);
                    }
                    current = current.BaseType;
                }
            }

            public system create_system ()
            {
                stack.Push (this);
                moon.system_domain.Push (null);
                author._creation();
                moon.system_domain.Pop ();
                stack.Pop ();

                return new system ( founder );
            }

        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class pathAttribute : Attribute
    {
        public string name { get; }

        public pathAttribute(string category)
        { name = category; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class linkAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Field)]
    public class exportAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Class)]
    public class inked : Attribute
    { }
}

// TODO: reflection cache for dependency