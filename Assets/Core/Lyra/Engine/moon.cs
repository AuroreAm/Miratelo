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

        static Action <moon> moon_constructor_event;
        
        public moon () {
            id = ++static_counter;

            if ( system.creator.on_creation )
            system.creator.add_brick ( this );
        }

        internal static Stack <system> system_domain = new Stack<system> ();

        protected virtual void _ready ()
        { }

        protected virtual void _destroy () { }

        /// <summary> internal call only </summary>
        internal virtual void destroy () {
            _destroy ();
            moon_destroyed (this);
        }

        internal void set_orbit ( system structure )
        {
            if (GetType().GetCustomAttribute<inkedAttribute>() != null && !ready)
                Debug.LogError($"{GetType().Name} was structured without package");

            system = structure;
            _ready ();
            moon_ready (this);
        }

        public abstract class ink <T> where T : moon, new()
        {
            public ink()
            {
                if (!system.creator.on_creation)
                    throw new InvalidOperationException("ink can be only instanced inside _creation ()");

                o = system.creator.querry<T>();
                o.ready = true;
            }
            public T o { private set; get; }
        }

        protected T a_new <T> () where T : moon, new () {
            T moon = new T ();
            system.add ( moon );
            return moon;
        }

        protected T with <T> ( T moon ) where T : moon {
            system.add ( moon );
            return moon;
        }

        
        // gloabl callback
        event Action<moon> moon_ready = delegate { };
        public event Action<moon> _moon_ready {
            add { moon_ready += value; }
            remove { moon_ready -= value; }
        }

        event Action<moon> moon_destroyed = delegate { };
        public event Action<moon> _moon_destroyed {
            add { moon_destroyed += value; }
            remove { moon_destroyed -= value; }
        }
    }

    public sealed class ink<T> where T : moon, new()
    {
        public ink ()
        {
            if (!system.creator.on_creation)
                throw new InvalidOperationException("ink can be only instanced inside _creation ()");

            o = system.creator.querry<T>();
        }

        public T o { private set; get; }
    }

    public class system
    {
        Dictionary<Type, moon> planets = new Dictionary<Type, moon>();
        List<moon> satellites = new List<moon>();

        private system( Dictionary<Type, moon> founders, List <moon> bricks )
        {
            satellites.AddRange (founders.Values);
            satellites.AddRange (bricks);
            satellites.RemoveDuplicates ();

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

        internal void add ( moon item )
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
                Debug.LogError($"no moon of type {typeof(T).Name} found in system {this}");
            return null;
        }

        /// <summary> stop all star and make this system invalid </summary>
        public void destroy () {
            foreach ( var s in satellites )
            s.destroy ();

            satellites = null;
            planets = null;
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
            internal static bool on_creation => stack.Count > 0;
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

            List <moon> bricks = new List<moon> ();
            internal static void add_brick ( moon moon ) {
                o.bricks.Add ( moon );
            } 

            public system create_system ()
            {
                bricks.Clear ();
                photon p = get_or_add_founder ( typeof (photon) ) as photon;

                stack.Push (this);
                author._create ();
                stack.Pop ();

                var s = new system ( founder, bricks );
                author._created (s);

                p.radiate ( new system_written ());

                return s;
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

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class inkedAttribute : Attribute
    { }

    public struct system_written {}
}

// TODO: reflection for dependency