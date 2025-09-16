using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace Lyra
{
    public class core : core_kind
    {
        int [] type_segment;
        List<Type> type_order;
        Dictionary < Type, Type > type_index = new Dictionary < Type, Type > ();
        List <star> stack = new List<star> ();

        public core ( Type [] _type_order )
        {
            type_order = new List<Type> ( _type_order );
            type_segment = new int [_type_order.Length];

            for (int i = 0; i < type_order.Count; i++)
            {
                type_index.Add ( type_order [i], type_order [i] );
                
                if (type_order [i].GetCustomAttribute<starAttribute>() != null)
                {
                    List <Type> deriveds = new List <Type> ();

                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                     deriveds.AddRange ( a.GetTypes().Where(type => type.IsSubclassOf(type_order [i]) ) );

                    foreach ( var a in deriveds )
                    type_index.Add ( a, type_order [i] );
                }
            }
        }

        void start ( star s )
        {
            if ( stack.Contains (s) )
            {
                Dev.Break ( $"star {s.GetType().Name} is already executed by phoenix" );
                return;
            }

            if ( ! type_index.ContainsKey ( s.GetType () ) )
            {
                Dev.Break ( $"this star can't be executed by phoenix, the type {s.GetType().Name} doesn't have a base star with excution order" );
                return;
            }

            Type target = type_index [s.GetType ()];
            int add_address = type_segment [ type_order.IndexOf (target) ];

            stack.AddAtIndex ( add_address, s );

            for (int i = type_order.IndexOf ( target ) + 1; i < type_segment.Length; i++)
            type_segment [i] ++;

            s.tick ( this );
        }

        public void execute ( star self )
        {
            start (self);
        }

        public void start_action ( action action )
        {
            start ( action );
        }

        internal void link ( star.main linked )
        {
            start ( linked );
        }


        public void _star_stop(star s)
        {
            if ( !stack.Contains (s) )
            {
                Debug.LogError ( "this star in not being executed by phoenix, but a" );
                return;
            }

            int AddressOf = stack.IndexOf ( s );
            stack.RemoveAt (AddressOf);
            
            for (int i = 0; i < type_segment.Length; i++)
            {
                if ( type_segment [i] > AddressOf )
                type_segment [i] --;
            }
        }

        star [] stack_cache;
        public void pulse ()
        {
            if (stack_cache == null || stack_cache.Length <= stack.Count)
            stack_cache = new star [ stack.Count + 200 ];

            int length = stack.Count;

            for (int i = 0; i < stack.Count; i++)
                stack_cache [i] = stack [i];

            for (int i = 0; i < length; i++)
            if (stack_cache [i].on)
            stack_cache [i].tick ( this );
        }
    }

    [AttributeUsage(AttributeTargets.Class,Inherited = false)]
    public class starAttribute : Attribute
    {
        public int order {get; private set;}

        public starAttribute ( int _order )
        {
            order = _order;
        }
    }
}