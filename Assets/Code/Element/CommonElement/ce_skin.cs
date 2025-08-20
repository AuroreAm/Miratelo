using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // Skin Element type: element of carbon based characters,
    // when skill is hit, it will directly reduce core HP
    // will call various type of message to the character based on the attack
    [Category("character")]
    public sealed class ce_skin : element
    {
    }

    public struct Damage
    {
        public float damage;

        public Damage ( float damage )
        {
            this.damage = damage;
        }
    }
}