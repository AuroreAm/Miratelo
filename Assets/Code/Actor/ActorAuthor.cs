using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorAuthor : MonoBehaviour, creator
    {
        public SkinAuthor Skin;
        public string Name;
        public int Faction;
        public ActionPaper Behavior;
 
        Vector3 _spam_position;
        float _spam_roty;

        public system spawn ( Vector3 position, Quaternion rotation )
        {
            _spam_position = position;
            _spam_roty =  rotation.eulerAngles.y;
            var s = new system.creator (this).create_system ();

            var modules = GetComponents<ActorAuthorModule>();
            foreach (var a in modules)
            a._creation (s);
            return s;
        }

        public void _creation()
        {
            GameObject go = new GameObject ( Name );
            go.transform.position = _spam_position;
            new character.ink ( go );

            new actor.ink ( Name );
            new warrior.ink (Faction);

            Instantiate ( Skin )._creation ();
            new ink <skin> ().o.roty = _spam_roty;

            new behavior.ink ( Behavior );

            var modules = GetComponents<ActorAuthorModule>();
            foreach (var a in modules)
            a._creation ();
        }

        void Start ()
        {
            spawn ( transform.position, transform.rotation );
            Destroy ( gameObject );
        }
    }
}