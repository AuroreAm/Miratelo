using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class GroundElement : PixIndex <p_ground_element>
    {
        public static GroundElement o;

        public GroundElement ()
        {
            o = this;
        }

        public static void Clash ( e_foot from, int to)
        {
          o.ptr[to].Clash ( from );
        }
    }

    public abstract class p_ground_element : indexed_pix <p_ground_element>
    {
        public abstract void Clash ( e_foot from );
    }

    public class pg_asphalt : p_ground_element
    {
        public static readonly term [] fts = new term [] { new term ("ft_asphalt0"), new term ("ft_asphalt1"), new term ("ft_asphalt2") };

        public override void Clash(e_foot from)
        {
            if (from is e_foot ef)
            ef.ss.PlaySFX ( fts [ Random.Range (0, fts.Length) ] );
        }
    }

    public class pg_wood : p_ground_element
    {
        public static readonly term [] fts = new term [] { new term ("ft_wood0"), new term ("ft_wood1"), new term ("ft_wood2") };

        public override void Clash(e_foot from)
        {
            if (from is e_foot ef)
            ef.ss.PlaySFX ( fts [ Random.Range (0, fts.Length) ] );
        }
    }
}