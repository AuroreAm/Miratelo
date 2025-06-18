using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class GroundElement : ThingPointer <p_ground_element>
    {
        public static GroundElement o;

        public GroundElement ()
        {
            o = this;
        }

        public static bool GroundExist ( int id )
        {
            return o.ptr.ContainsKey ( id );
        }

        public static void Clash ( e_foot from, int to)
        {
          o.ptr[to].Clash ( from );
        }
    }

    public abstract class p_ground_element : thingptr <p_ground_element>
    {
        public abstract void Clash ( e_foot from );
        public sealed override bool Main()
        {
            return false;
        }
    }

    public class pg_asphalt : p_ground_element
    {
        public static readonly SuperKey [] fts = new SuperKey [] { new SuperKey ("ft_asphalt0"), new SuperKey ("ft_asphalt1"), new SuperKey ("ft_asphalt2") };

        public override void Clash(e_foot from)
        {
            if (from is e_foot ef)
            ef.ms.PlaySFX ( fts [ Random.Range (0, fts.Length) ] );
        }
    }

    public class pg_wood : p_ground_element
    {
        public static readonly SuperKey [] fts = new SuperKey [] { new SuperKey ("ft_wood0"), new SuperKey ("ft_wood1"), new SuperKey ("ft_wood2") };

        public override void Clash(e_foot from)
        {
            if (from is e_foot ef)
            ef.ms.PlaySFX ( fts [ Random.Range (0, fts.Length) ] );
        }
    }
}