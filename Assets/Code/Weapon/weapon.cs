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
        public actor owner { get; protected set; }
        public Transform coord => c.gameobject.transform;

        public void aquire (actor Owner)
        {
            if (owner) Debug.LogError ("Weapon already owned");
            owner = Owner;
            c.gameobject.layer = 0;
        }
    }
}