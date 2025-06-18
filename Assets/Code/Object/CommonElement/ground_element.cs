using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class eg_asphalt : element
    {
        public static readonly SuperKey [] fts = new SuperKey [] { new SuperKey ("ft_asphalt0"), new SuperKey ("ft_asphalt1"), new SuperKey ("ft_asphalt2") };

        public override void Clash(element from, Force force)
        {
            if (from is e_foot ef)
            ef.ms.PlaySFX ( fts [ Random.Range (0, fts.Length) ] );
        }

        public override void ReverseClash(element from, Force force)
        {
        }
    }

    public class eg_wood : element
    {
        public static readonly SuperKey [] fts = new SuperKey [] { new SuperKey ("ft_wood0"), new SuperKey ("ft_wood1"), new SuperKey ("ft_wood2") };

        public override void Clash(element from, Force force)
        {
            if (from is e_foot ef)
            ef.ms.PlaySFX ( fts [ Random.Range (0, fts.Length) ] );
        }

        public override void ReverseClash(element from, Force force)
        {
        }
    }
}