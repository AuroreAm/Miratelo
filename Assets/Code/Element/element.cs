using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("element")]
    public abstract class element : public_moon <element>
    {
        [link]
        public photon photon;

        [link]
        public warrior warrior;
    }
}