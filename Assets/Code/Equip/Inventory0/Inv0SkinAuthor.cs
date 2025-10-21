using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class Inv0SkinAuthor : SkinWriterModule
    {
        public Transform [] SwordPlaces;
        public Transform [] BowPlaces;

        protected override void _create ()
        {
            new inv0.ink ( SwordPlaces, BowPlaces );
        }
    }
}