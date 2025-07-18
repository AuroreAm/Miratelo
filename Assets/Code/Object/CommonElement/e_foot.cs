using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class e_foot : pix
    {
        public FootType type;

        [Depend]
        public s_sfx ss;
    }

    public enum FootType { normal, metal, bare }
}