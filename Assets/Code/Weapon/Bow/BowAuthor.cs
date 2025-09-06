using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class BowAuthor : WeaponAuthor
    {
        public string arrow;
        public Transform BowString;

        protected override weapon __creation()
        {
            return new bow.ink ( BowString, arrow ).o;
        }
    }
}
