using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class e_foot : module
    {
        public FootType type;

        [Depend]
        public m_sfx ms;
    }

    public enum FootType { normal, metal, bare }
}