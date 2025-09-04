using Lyra;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    [inkedAttribute]
    public class d_sword : d_weapon_standard
    {
        float length;
        term slash;

        public class package : ink <d_sword>
        {
            public package ( float length, string slashName )
            {
                o.length = length;
                o.slash = new term ( slashName );
            }
        }

    }
}
