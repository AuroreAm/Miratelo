using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class e_foot : element
    {
        public FootType type;

        [Depend]
        public m_sfx ms;

        public override void Clash(element from, Force force)
        {
        }

        public override void ReverseClash(element from, Force force)
        {
        }
    }

    public enum FootType { normal, metal, bare }
}