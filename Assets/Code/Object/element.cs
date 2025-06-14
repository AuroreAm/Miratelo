using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{

    public class m_element : module
    {
        public element element { private set; get; }

        public void SetElement ( element e )
        {
            character.ConnectNode ( e );
            element = e;
        }
    }

    public abstract class element : node
    {
        public abstract void Clash ( element from, element to, Force force);
    }

}