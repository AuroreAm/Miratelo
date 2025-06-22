using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class Element : ModulePointer<m_element>
    {
        public static Element o;

        public static void Clash ( element from, int to, Force force = default(Force) )
        {
            o.ptr[to].element.Clash ( from, force );
            from.ReverseClash ( o.ptr[to].element, force );
        }

        public Element ()
        {
            o = this;
        }
    }
}