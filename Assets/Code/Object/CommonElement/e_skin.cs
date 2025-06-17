using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class e_skin : element
    {
        public override void Clash( m_element host, element from, Force force )
        {
            host.OnClash?.Invoke ( new ClashEvent ( force, from, HitType.Small ) );
        }
    }
}
