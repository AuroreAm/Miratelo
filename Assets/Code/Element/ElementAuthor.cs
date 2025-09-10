using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ElementAuthor : SkinAuthorModule
    {
        public moon_paper <element> Element;

        public override void _creation()
        {}

        public override void _creation(system system)
        {
            system.add ( Element.write () );
        }
    }
}
