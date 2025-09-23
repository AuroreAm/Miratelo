using Lyra;

namespace Triheroes.Code.Acting
{
    [path("acting")]
    public class move : acting {
        [link]
        Code.move move_act;

        protected override act get_act() => move_act;
    }
}