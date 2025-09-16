using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code.Element
{
    public class skin : element, ruby <perce>
    {
        public void _radiate ( perce gleam )
        {
            Debug.Log ("hit");
        }

        protected override void _ready()
        {
            register ( system.get <character> ().gameobject.GetInstanceID () );
        }
    }
}