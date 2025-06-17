using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // for testing purposes
    public class ac_idle : action
    {
        [Depend]
        m_capsule_character_controller mccc;

        protected override void BeginStep()
        {
            mccc.Aquire (this);
        }

        protected override void Stop()
        {
            mccc.Free (this);
        }
    }
}
