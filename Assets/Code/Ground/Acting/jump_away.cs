using Lyra;

namespace Triheroes.Code.Acting {
    [path ("acting")]
    public class jump_away : acting.first {
        [link]
        Code.jump_away jump;

        protected override act get_act() => jump;
    }
}