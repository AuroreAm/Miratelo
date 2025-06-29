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
        public m_HP mhp;

        [Depend]
        m_last_knock mlk;

        [Depend]
        m_element me;

        public override void Clash(element from, Slash force)
        {
            float damage = force.raw * force.sharpness;
            mhp.HP -= damage;
        }

        public override void Clash(element from, Knock force)
        {
            mlk.LastKnockDir = force.Dir;
            me.SendMessage ( MessageKey.knock_forced );
        }
    }
}