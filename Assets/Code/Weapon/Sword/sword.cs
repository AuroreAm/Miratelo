using Lyra;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    [inkedPackage]
    public class sword : weapon
    {
        float length;
        term slash;

        public class ink : ink < sword >
        {
            public ink ( float length, string slash_name )
            {
                o.length = length;
                o.slash = new term ( slash_name );
            }
        }

    }
}
