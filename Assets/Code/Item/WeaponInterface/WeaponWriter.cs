using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    public class WeaponWriter : MonoBehaviour
    {
        void Awake ()
        {
            GetComponent<Weapon> ().element = new e_metal ();
        }
    }
}