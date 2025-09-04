using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class ActorAuthor : MonoBehaviour, IStructureAuthor
    {
        public SkinAuthor Skin;
        public string Name;
        public ActionPaper Behavior;
 
        Vector3 _spamPosition;
        float _spamRotY;

        public dat.structure Spawn ( Vector3 position, Quaternion rotation )
        {
            _spamPosition = position;
            _spamRotY =  rotation.eulerAngles.y;
            var s = new dat.structure.Creator (this).CreateStructure ();

            var modules = GetComponents<ActorAuthorModule>();
            foreach (var a in modules)
            a.OnStructureReady (s);
            return s;
        }

        public void OnStructure()
        {
            GameObject go = new GameObject ( Name );
            go.transform.position = _spamPosition;
            new character.package ( go );

            new d_actor.package ( Name );

            Instantiate ( Skin ).OnStructure ();
            dat.Q <s_skin> ().RotY = _spamRotY;

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