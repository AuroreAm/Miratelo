using Lyra;

namespace Triheroes.Code.Mecha {
    [path ("mecha")]
    public class dash_slay : acting.first {
        mecha_dash_slay slay;

        protected override void __ready() {
            slay = a_new < mecha_dash_slay > ();
        }

        protected override act get_act() => slay;
    }
}