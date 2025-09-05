using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class Inv0SkinAuthor : SkinAuthorModule
    {
        public Transform [] SwordPlaces;
        public Transform [] BowPlaces;

        public override void _creation()
        {
            new inv0.ink ( SwordPlaces, BowPlaces );
        }
    }
}