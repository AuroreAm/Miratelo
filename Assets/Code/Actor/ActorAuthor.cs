using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class ActorAuthor : CharacterAuthor, creator {
        public string Name;
        public int Faction;
        public SkinWriter Skin;

        Vector3 _spam_position;
        float _spam_roty;
        SkinWriter _skin;

        List < ActorAuthorModule > modules;

        public override void Spawn (){
            Spawn ( transform.position, transform.rotation );
        }

        public system Spawn ( Vector3 position, Quaternion rotation ) {
            _spam_position = position;
            _spam_roty =  rotation.eulerAngles.y;

            var s = new system.creator (this).create_system ();
            return s;
        }

        public void _create() {
            modules = new List<ActorAuthorModule> ( GetComponents <ActorAuthorModule> () );

            GameObject go = new GameObject ( Name );
            go.transform.position = _spam_position;
            new character.ink ( go );

            new actor.ink ( Name );
            new warrior.ink (Faction);
            new ink <behavior> ();

            _skin = Instantiate (Skin); _skin.Create ();
            // skin rotation
            new ink <skin> ().o.roty = _spam_roty;

            foreach (var a in modules)
            a._create ();
        }

        public void _created(system s) {
            _skin.Created (s);
            foreach (var a in modules)
            a._created (s);
        }
    }
}