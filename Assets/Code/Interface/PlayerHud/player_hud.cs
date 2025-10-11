using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class player_hud : moon {
        
        public RectTransform heart_container;

        public class ink : ink <player_hud> {
            public ink ( RectTransform _heart_container ) { 
                o.heart_container = _heart_container;
            }
        }

    }
}