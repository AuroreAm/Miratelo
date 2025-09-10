using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class StatAuthor : ActorAuthorModule
    {
        public float MaxHP;

        public override void _creation()
        {
            new ink <health_point> ().o.set ( MaxHP );
        }
        
    }
}
