using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class Stage : MonoBehaviour, IPixiHandler, IBlockAuthor
    {
        public static Stage o;
        public static block Director {private set; get;}

        static int counter;
        List <pixi> Pixis = new List<pixi> ();
        Dictionary <int, pixi> PixiIndex = new Dictionary<int, pixi> ();
        Dictionary <pixi, int> IndexPixi = new Dictionary<pixi, int> ();

        int [] TypeAddress;
        List<Type> TypeOrder;
        Dictionary < Type, Type > TypeIndex = new Dictionary<Type, Type> ();

        public abstract void SetDirectorPix ( in List <Type> a );
        public abstract void StageStart ();

        void Awake ()
        {
            o = this;

            TypeOrder = new List<Type> (PixiExecutionOrder ());
            TypeAddress = new int [TypeOrder.Count];

            for (int i = 0; i < TypeOrder.Count; i++)
            {
                TypeIndex.Add ( TypeOrder [i], TypeOrder [i] );
                
                if (TypeOrder [i].GetCustomAttribute<PixiBaseAttribute>() != null)
                {
                    List <Type> deriveds = new List <Type> ();

                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                     deriveds.AddRange ( a.GetTypes().Where(type => type.IsSubclassOf(TypeOrder [i]) ) );

                    foreach ( var a in deriveds )
                    TypeIndex.Add ( a, TypeOrder [i] );
                }
            }

            // set directioor there with its module
            List <Type> pixes = new List<Type> ();
            SetDirectorPix ( in pixes );
            Director = new PreBlock ( pixes.ToArray (), this ).CreateBlock ();

        }

        void Start ()
        {
            StageStart ();
        }

        public static int Start ( pixi brick ) => o._Start (brick);
        int _Start ( pixi brick )
        {
            int StartId = counter + 1;
            counter ++;

            Type TypeTarget = TypeIndex [brick.GetType ()];
            int AddingAdress = TypeAddress [ TypeOrder.IndexOf (TypeTarget) ];

            PixiIndex.Add ( StartId, brick );
            IndexPixi.Add ( brick, StartId );
            Pixis.AddAtIndex ( AddingAdress, brick );

            for (int i = TypeOrder.IndexOf ( TypeTarget ) + 1; i < TypeAddress.Length; i++)
            TypeAddress [i] ++;

            brick.Tick (this);
            return StartId;
        }

        public static void Stop ( int id ) => o._Stop (id);
        void _Stop ( int id )
        {
            PixiIndex [id].ForceStop (this);
        }

        protected abstract Type [] PixiExecutionOrder ();

        pixi [] cache;
        void LateUpdate ()
        {
            if (cache == null || cache.Length <= Pixis.Count)
            cache = new pixi [ Pixis.Count + 200 ];

            int length = Pixis.Count;

            for (int i = 0; i < Pixis.Count; i++)
            {
                cache [i] = Pixis [i];
            }

            for (int i = 0; i < length; i++)
            if (cache [i].on)
            cache [i].Tick (this);
        }

        public void OnPixiEnd(pixi p)
        {
            int AddressOf = Pixis.IndexOf ( p );
            Pixis.RemoveAt (AddressOf);
            
            for (int i = 0; i < TypeAddress.Length; i++)
            {
                if ( TypeAddress [i] > AddressOf )
                TypeAddress [i] --;
            }

            PixiIndex.Remove ( IndexPixi [p] );
            IndexPixi.Remove (p);
        }

        public void OnWriteBlock()
        {}
    }
}
