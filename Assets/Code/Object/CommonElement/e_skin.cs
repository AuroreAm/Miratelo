using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // Skin Element type: element of carbon based characters,
    // when skill is hit, it will directly reduce core HP
    // will call various type of message to the character based on the attack

    public sealed class e_skin : element
    {
        [Depend]
        m_stat_HP mshp;

        public override void Clash( element from, Force force )
        {
        }

        public override void ReverseClash(element from, Force force)
        {
        }
    }
}
