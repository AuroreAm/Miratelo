using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class element : public_moon <element>
    {
        [link]
        public photon photon;

        [link]
        public warrior warrior;
    }
}