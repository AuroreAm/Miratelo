using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // TODO: remove s_element from systems because there's nothing executed here
    public class s_element : ModulePointerSystem<m_element>
    {
        public static s_element o;

        public static void Clash ( element from, int to, Force force = default(Force) )
        {
            o.ptr[to].element.Clash ( from, force );
            from.ReverseClash ( o.ptr[to].element, force );
        }

        public s_element ()
        {
            o = this;
        }

        public override void Execute()
        {}
    }
}
