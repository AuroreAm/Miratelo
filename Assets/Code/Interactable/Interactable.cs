using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class Interactable : PixIndex <p_interactable>
    {
        static Interactable o;
        public override void Create()
        {
            o = this;
        }

        public static p_interactable GetInteractable ( int id )
        {
            return o.ptr[id];
        }
    }
}