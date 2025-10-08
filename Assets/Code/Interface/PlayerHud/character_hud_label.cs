using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class character_hud_label : moon {
        Text text;

        public character_hud_label ( Text _text ) {
            text = _text;
        }

        public void set (actor actor) {
            text.text = actor.name;
        }
    }
}