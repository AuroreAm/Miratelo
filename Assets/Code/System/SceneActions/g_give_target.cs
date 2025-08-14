using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify.Spirit;
using Pixify;

namespace Triheroes.Code
{
    [Category ("actor")]
    public class g_give_target : action
    {
        [Export]
        public string Actor;
        [Export]
        public string Target;

        protected override void Start()
        {
            ActorList.Get ( Actor ).LockATarget ( ActorList.Get ( Target ) );
        }
    }
}
