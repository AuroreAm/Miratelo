using Lyra;

namespace Triheroes.Code.Mecha
{
    [path ("mecha ai")]
    public class ai_charge : action
    {
        [link]
        mecha_buster.charger charge;

        protected override void _start()
        {
            link ( charge );
        }

        protected override void _step()
        {
            if ( charge.charged )
            stop ();
        }
    }
}