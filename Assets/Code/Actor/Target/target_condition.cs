using Lyra;
using Lyra.Spirit;

namespace Triheroes.Code
{
    [Category("actor")]
    public class target_condition : condition
    {
        [Depend]
        d_actor da;

        public override bool Check()
        {
            return da.target;
        }
    }
}