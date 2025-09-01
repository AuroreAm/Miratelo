using Lyra;
using Lyra.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    [Category ("script")]
    public class r_of_parry : reaction, IElementListener<parried>
    {
        public void OnMessage(parried context)
        {
            if (!on)
            return;

            Debug.Log ( "parried" );
        }
    }
}
