using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // Skin Element type: element of carbon based characters,
    [Category("character")]
    public sealed class ce_skin : element, IElementListener <Slash>
    {
        [Depend]
        s_element se;

        public void OnMessage(Slash context)
        {
            se.SendMessage (new Damage (context.raw));
            Debug.Log ("hit");
        }
    }
}