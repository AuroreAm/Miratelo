using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class terra : index < ground_element>
    {
        static terra o;

        public terra ()
        {
            o = this;
        }

        public static void clash ( foot from, int to)
        {
          o.ptr[to].clash ( from );
        }
    }

    public abstract class ground_element : public_moon <ground_element>
    {
        public abstract void clash ( foot from );
    }

    public class foot : moon
    {
        [link]
        public actor_sfx sfx;

        public enum foot_type { normal, metal, bare }
        public foot_type type;
    }

    public class pg_asphalt : ground_element
    {
        public static readonly term [] fts = new term [] { new term ("ft_asphalt0"), new term ("ft_asphalt1"), new term ("ft_asphalt2") };

        public override void clash (foot from)
        {
            if (from is foot ef)
            ef.sfx.play ( fts [ Random.Range (0, fts.Length) ] );
        }
    }

    public class pg_wood : ground_element
    {
        public static readonly term [] fts = new term [] { new term ("ft_wood0"), new term ("ft_wood1"), new term ("ft_wood2") };

        public override void clash (foot from)
        {
            if (from is foot ef)
            ef.sfx.play ( fts [ Random.Range (0, fts.Length) ] );
        }
    }

}
