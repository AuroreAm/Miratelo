using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class s_element : ModulePointerSystem<m_element>
    {
        public static s_element o;

        public void Clash ( element from,int to, Force force )
        {
            if ( ptr.TryGetValue (  to, out m_element e ) )
            e.Clash ( from, force );
        }

        public s_element ()
        {
            o = this;
        }

        public override void Execute()
        {

        }
    }
}
