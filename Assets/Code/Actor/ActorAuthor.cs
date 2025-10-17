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
 
        List <AuthorModule> modules;

        Vector3 _spam_position;
        float _spam_roty;

        public system spawn ( Vector3 position, Quaternion rotation )
        {
            _spam_position = position;
            _spam_roty =  rotation.eulerAngles.y;

            var s = new system.creator (this).create_system ();
            return s;
        }

        public void _create()
        {
            modules = new List<AuthorModule> ( GetComponents <ActorAuthorModule> () );

            GameObject go = new GameObject ( Name );
            go.transform.position = _spam_position;
            new character.ink ( go );

            new actor.ink ( Name );
            new warrior.ink (Faction);

            modules.Add ( Instantiate ( Skin ) );

            new ink <script> ();
            if (Scripts)
            modules.Add ( Scripts );

            foreach (var a in modules)
            a._create ();

            // skin rotation
            new ink <skin> ().o.roty = _spam_roty;
        }

        public void _created (system s)
        {
            foreach (var a in modules)
            a._created (s);
        }

        void Awake ()
        {
            spawn ( transform.position, transform.rotation );
            Destroy ( this.gameObject );
        }
    }
}