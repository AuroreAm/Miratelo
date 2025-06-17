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
        m_character_controller mcc;

        void Awake ()
        {
            mcc = Director.o.ConnectNode ( new m_character_controller () );
            
            TreeStart ( Director.o );
            mcc.root = script.CreateTree ();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.id() == GameData.o.MainActors[0].character.gameObject.GetInstanceID())
            {
                mcc.Aquire (new Void ());
                Destroy (gameObject);
            }
        }

    }
}