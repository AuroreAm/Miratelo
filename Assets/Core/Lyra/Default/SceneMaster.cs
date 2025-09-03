using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;

namespace Lyra
{
    [DefaultExecutionOrder(-1500)]
    public sealed class SceneMaster : MonoBehaviour, IStructureAuthor
    {
        public static dat.structure Master {private set; get;}

        public static Processor Processor;

        void Awake ()
        {
            LoadTypeOrder ();
            LoadMaster ();
        }

        void LateUpdate ()
        {
            Processor.Tick ();
        }

        void LoadTypeOrder ()
        {
            Type [] _typeOrder =
            AppDomain.CurrentDomain.GetAssemblies ().SelectMany ( a => a.GetTypes () )
            .Where ( t => t.GetCustomAttribute <SysBaseAttribute> () != null )
            .OrderBy ( t => t.GetCustomAttribute<SysBaseAttribute> ()!.Order )
            .ToArray ();

            Processor = new Processor ( _typeOrder );
        }

        void LoadMaster ()
        {
            var A = AppDomain.CurrentDomain.GetAssemblies();
            List <Type> DatWithSceneMaster = new List<Type> ();

            foreach (var y in A)
            foreach (Type x in y.GetTypes())
            {
                if ( x.GetCustomAttribute <InitializeWithSceneMasterAttribute> () != null )
                DatWithSceneMaster.Add (x);
            }
            
            var MasterCreator = new dat.structure.Creator ( DatWithSceneMaster.ToArray (), this );
            Master = MasterCreator.CreateStructure ();
        }

        public void OnStructure() {}
    }

    public sealed class InitializeWithSceneMasterAttribute : Attribute
    {}
}