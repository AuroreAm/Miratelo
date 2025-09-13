using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public sealed class ActorAuthor : MonoBehaviour, creator
    {
        public SkinAuthor Skin;
        public string Name;
        public int Faction;

        public ScriptAuthor Scripts;
        public string StartScript;
 
        List <AuthorModule> modules;

        Vector3 _spam_position;
        float _spam_roty;

        public system spawn ( Vector3 position, Quaternion rotation )
        {
            _spam_position = position;
            _spam_roty =  rotation.eulerAngles.y;

            var s = new system.creator (this).create_system ();

            foreach (var a in modules)
            a._creation (s);

            s.get <photon> ().radiate ( new actor_created () );

            return s;
        }

        public void _creation()
        {
            modules = new List<AuthorModule> ( GetComponents <ActorAuthorModule> () );

            GameObject go = new GameObject ( Name );
            go.transform.position = _spam_position;
            new character.ink ( go );

            new actor.ink ( Name );
            new warrior.ink (Faction);

            modules.Add ( Instantiate ( Skin ) );
            modules.Add ( Scripts );

            new behavior.ink ( new term (StartScript) );

            new ink <photon> ();

            foreach (var a in modules)
            a._creation ();

            // skin rotation
            new ink <skin> ().o.roty = _spam_roty;
        }

        void Start ()
        {
            spawn ( transform.position, transform.rotation );
            Destroy ( this.gameObject );
        }
    }

    public struct actor_created {}
}