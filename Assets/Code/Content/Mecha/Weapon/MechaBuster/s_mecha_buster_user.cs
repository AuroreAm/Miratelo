using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class d_mecha_buster : pix
    {

        Transform BusterOrigin, BusterEnd;
        
        public Vector3 BusterOriginPosition => BusterOrigin.position;

        public float rotY => Vecteur.RotDirectionY(BusterOrigin.position, BusterEnd.position);

        public class package : PreBlock.Package <d_mecha_buster>
        {
            public package ( Transform BusterOrigin, Transform BusterEnd ) 
            {
                o.BusterOrigin = BusterOrigin;
                o.BusterEnd = BusterEnd;
            }
        }
    }
}