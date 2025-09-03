using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class Inv0SkinAuthor : SkinAuthorModule
    {
        public Transform [] SwordPlaces;
        public Transform [] BowPlaces;

        public override void OnStructure()
        {
            new d_inv_0.package ( SwordPlaces, BowPlaces );
        }
    }
}