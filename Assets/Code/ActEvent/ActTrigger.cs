using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using static Pixify.treeBuilder;

namespace Triheroes.Code
{
    
    // start a script when the main character enters a trigger
    public class ActTrigger : MonoBehaviour
    {
        public script script;
        action triggerRoot;
        m_character_controller mcc;

        void Awake ()
        {
            mcc = Director.o.ConnectNode ( new m_character_controller () );
            
            TreeStart ( Director.o );
            triggerRoot = script.CreateTree ();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.id() == GameData.o.MainActors[0].character.gameObject.GetInstanceID())
            {
                mcc.StartRoot ( triggerRoot );
                Destroy (gameObject);
            }
        }
    }
}