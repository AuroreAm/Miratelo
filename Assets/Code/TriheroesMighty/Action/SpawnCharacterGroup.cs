using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SpawnCharacterGroup : CustomActionPaper<spawn_character_group> {
        public GameObject group;
        protected override spawn_character_group Instance() => new spawn_character_group (group);
    }

    [paper (typeof (SpawnCharacterGroup))]
    [path("scene")]
    public class spawn_character_group : action {

        GameObject group;

        public spawn_character_group ( GameObject _group ) {
            group = _group;
        }

        protected override void _start() {
            CharacterAuthor [] g = group.GetComponentsInChildren <CharacterAuthor> ();
            foreach ( var a in g )
            a.Spawn ();

            stop ();
        }
    }
}