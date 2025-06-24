using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class Door : MonoBehaviour
    {
        void Start ()
        {

            Destroy (this);
        }
    }

    public class p_door : p_interactable
    {
        
    }
}
