using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SpawnActorGroup : CustomActionPaper<spawn_actor_group> {
        public GameObject group;
        protected override spawn_actor_group Instance() => new spawn_actor_group (group);
    }

    [paper (typeof (SpawnActorGroup))]
    [path("scene")]
    public class spawn_actor_group : action {

        GameObject group;

        public spawn_actor_group ( GameObject _group ) {
            group = _group;
        }

        protected override void _start() {
            ActorAuthor [] g = group.GetComponentsInChildren <ActorAuthor> ();
            foreach ( var a in g )
            a.Spawn ();

            stop ();
        }
    }
}