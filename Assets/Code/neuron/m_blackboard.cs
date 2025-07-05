using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_blackboard : module
    {
        public Dictionary <SuperKey, bool> BOOL {private set; get;} = new Dictionary<SuperKey, bool>();
        public Dictionary <SuperKey, SuperKey> KEY {private set; get;} = new Dictionary<SuperKey, SuperKey> ();
    }
}