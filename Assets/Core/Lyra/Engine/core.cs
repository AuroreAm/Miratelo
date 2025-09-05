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

        List <star> stack = new List<star> ();
        
        Dictionary < star, List <star.main> >  link_web = new Dictionary <star, List<star.main>> ();

        Queue < List < star.main > > star_link_pool = new Queue<List<star.main>> ();
        List < star.main > rent_star_link () => star_link_pool.Count > 0 ? star_link_pool.Dequeue () : new List<star.main> ();

        void return_star_link ( List <star.main> star_link )
        {
            star_link.Clear ();
            star_link_pool.Enqueue (star_link);
        }

        public void start ( star s )
        {
            if ( stack.Contains (s) )
            {
                Debug.LogError ( "this star is already started, make sure it's stopped before starting it" );
                return;
            }

            if ( ! type_index.ContainsKey ( s.GetType () ) )
            {
                Debug.LogError ( $"this star cannot be processed, the type {s.GetType().Name} is missing in the core order" );
                return;
            }

            Type target = type_index [s.GetType ()];
            int add_address = type_segment [ type_order.IndexOf (target) ];

            stack.AddAtIndex ( add_address, s );

            for (int i = type_order.IndexOf ( target ) + 1; i < type_segment.Length; i++)
            type_segment [i] ++;

            link_web.Add ( s, rent_star_link () );

            s.tick (this);
        }

        /// <summary>
        /// tell the core that this star is started but not executed by this core
        /// this is used for link
        /// </summary>
        public void fake_start ( star s )
        {
            link_web.Add ( s, rent_star_link () );
        }

        public void fake_stop ( star s )
        {
            foreach ( star link in link_web [s] )
                link.stop (this);

            return_star_link ( link_web [s] );
            link_web.Remove (s);
        }

        public void link ( star host, star.main linked )
        {
            link_web [host].Add ( linked );
            start (linked);
        }

        public void unlink ( star host, star.main linked )
        {
            link_web [host].Remove (linked);
            linked.stop ( this );
        }

        public void _star_stop(star s)
        {
            if ( !stack.Contains (s) )
            {
                Debug.LogError ( "this sys is already stopped" );
                return;
            }

            int AddressOf = stack.IndexOf ( s );
            stack.RemoveAt (AddressOf);

            foreach ( star link in link_web [s] )
                link.stop (this);

            return_star_link ( link_web [s] );
            link_web.Remove (s);
            
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
            stack_cache [i].tick (this);
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

    public static class starExtensions
    {
        /// <summary>
        /// start another sys and link with this, the linked sys stop when the host is stopped
        /// </summary>
        public static void link ( this star s, star.main link )
        {
            phoenix.core.link ( s, link );
        }

        public static void unlink ( this star s, star.main link )
        {
            phoenix.core.unlink ( s, link );
        }
    }
}