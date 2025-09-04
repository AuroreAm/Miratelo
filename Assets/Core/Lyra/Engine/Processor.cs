using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace Lyra
{
    public class Processor : ISysProcessor
    {
        int [] TypeAddress;
        List<Type> TypeOrder;
        Dictionary < Type, Type > TypeIndex = new Dictionary<Type, Type> ();

        public Processor ( Type [] typeOrder )
        {
            TypeOrder = new List<Type> (typeOrder);
            TypeAddress = new int [typeOrder.Length];

            for (int i = 0; i < TypeOrder.Count; i++)
            {
                TypeIndex.Add ( TypeOrder [i], TypeOrder [i] );
                
                if (TypeOrder [i].GetCustomAttribute<SysBaseAttribute>() != null)
                {
                    List <Type> deriveds = new List <Type> ();

                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                     deriveds.AddRange ( a.GetTypes().Where(type => type.IsSubclassOf(TypeOrder [i]) ) );

                    foreach ( var a in deriveds )
                    TypeIndex.Add ( a, TypeOrder [i] );
                }
            }
        }

        List <sys> _stack = new List<sys> ();
        
        Dictionary < sys, List <sys.ext> >  _link = new Dictionary<sys, List<sys.ext>> ();

        Queue < List < sys.ext > > _poolListSys = new Queue<List<sys.ext>> ();
        List < sys.ext > RentSysList () => _poolListSys.Count > 0 ? _poolListSys.Dequeue () : new List<sys.ext> ();

        void ReturnSysList ( List <sys.ext> sys )
        {
            sys.Clear ();
            _poolListSys.Enqueue (sys);
        }

        public void Start ( sys s )
        {
            if ( _stack.Contains (s) )
            {
                Debug.LogError ( "this sys is already started, make sure it's stopped before starting it" );
                return;
            }

            if ( ! TypeIndex.ContainsKey ( s.GetType () ) )
            {
                Debug.LogError ( $"this sys cannot be processed, the type {s.GetType().Name} is missing in the scenemaster order" );
                return;
            }

            Type TypeTarget = TypeIndex [s.GetType ()];
            int AddingAdress = TypeAddress [ TypeOrder.IndexOf (TypeTarget) ];

            _stack.AddAtIndex ( AddingAdress, s );

            for (int i = TypeOrder.IndexOf ( TypeTarget ) + 1; i < TypeAddress.Length; i++)
            TypeAddress [i] ++;

            _link.Add ( s, RentSysList () );

            s.Tick (this);
        }

        /// <summary>
        /// tell the processor that this sys is started but not executed by this processor
        /// this is used for link
        /// </summary>
        public void FakeStart ( sys s )
        {
            _link.Add ( s, RentSysList () );
        }

        public void FakeEnd ( sys s )
        {
            foreach ( sys link in _link [s] )
                link.ForceStop (this);

            ReturnSysList ( _link [s] );
            _link.Remove (s);
        }

        public void Link ( sys host, sys.ext linked )
        {
            _link [host].Add ( linked );
            Start (linked);
        }

        public void Unlink ( sys host, sys.ext linked )
        {
            _link [host].Remove (linked);
            linked.ForceStop ( this );
        }

        public void OnSysEnd(sys s)
        {
            if ( !_stack.Contains (s) )
            {
                Debug.LogError ( "this sys is already stopped" );
                return;
            }

            int AddressOf = _stack.IndexOf ( s );
            _stack.RemoveAt (AddressOf);

            foreach ( sys link in _link [s] )
                link.ForceStop (this);

            ReturnSysList ( _link [s] );
            _link.Remove (s);
            
            for (int i = 0; i < TypeAddress.Length; i++)
            {
                if ( TypeAddress [i] > AddressOf )
                TypeAddress [i] --;
            }
        }

        sys [] _stackCache;
        public void Tick ()
        {
            if (_stackCache == null || _stackCache.Length <= _stack.Count)
            _stackCache = new sys [ _stack.Count + 200 ];

            int length = _stack.Count;

            for (int i = 0; i < _stack.Count; i++)
                _stackCache [i] = _stack [i];

            for (int i = 0; i < length; i++)
            if (_stackCache [i].on)
            _stackCache [i].Tick (this);
        }
    }

    [AttributeUsage(AttributeTargets.Class,Inherited = false)]
    public class SysBaseAttribute : Attribute
    {
        public int Order {get; private set;}

        public SysBaseAttribute ( int order )
        {
            Order = order;
        }
    }

    public static class sysExtensions
    {
        /// <summary>
        /// start another sys and link with this, the linked sys stop when the host is stopped
        /// </summary>
        public static void Link ( this sys s, sys.ext link )
        {
            SceneMaster.Processor.Link ( s, link );
        }

        public static void Unlink ( this sys s, sys.ext link )
        {
            SceneMaster.Processor.Unlink ( s, link );
        }
    }
}