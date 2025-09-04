using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorAuthor : MonoBehaviour, IAuthor
    {
        public SkinAuthor Skin;
        public string Name;
        public ActPaper Behavior;
 
        Vector3 _spamPosition;
        float _spamRotY;

        public shard.constelation Spawn ( Vector3 position, Quaternion rotation )
        {
            _spamPosition = position;
            _spamRotY =  rotation.eulerAngles.y;
            var s = new shard.constelation.write (this).constelation ();

            var modules = GetComponents<ActorAuthorModule>();
            foreach (var a in modules)
            a.OnStructureReady (s);
            return s;
        }

        public void writings()
        {
            GameObject go = new GameObject ( Name );
            go.transform.position = _spamPosition;
            new character.package ( go );

            new d_actor.package ( Name );

            Instantiate ( Skin ).OnStructure ();
            new ink <s_skin> ().o.RotY = _spamRotY;

            new s_behavior.package ( Behavior );

            var modules = GetComponents<ActorAuthorModule>();
            foreach (var a in modules)
            a.OnStructure ();
        }

        void Start ()
        {
            Spawn ( transform.position, transform.rotation );

            Destroy ( gameObject );
        }
    }
}