using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    
    // start a script when the main character enters a trigger
    public class ActTrigger : MonoBehaviour
    {
        public script script;
        action triggerRoot;

        void Awake ()
        {
            triggerRoot = script.WriteTree (Director.o);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.id() == play.MainCharacter.gameObject.GetInstanceID())
            {
                Act.Start ( triggerRoot );
                Destroy (gameObject);
            }
        }
    }
}