using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lyra;

namespace Triheroes.Code
{
    public abstract class weapon : moon
    {
        [link]
        character c;
        public warrior owner { get; protected set; }
        public Transform coord => c.gameobject.transform;

        public void aquire (warrior _owner)
        {
            if ( owner ) Debug.LogError ("Weapon already owned");

            owner = _owner;
            c.gameobject.layer = 0;
        }
    }
}