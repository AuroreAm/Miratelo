using Lyra;
using UnityEngine;

namespace Triheroes.Code.Mecha
{
    [path ("mecha")]
    public class charge_S1 : acting.first {
        [link]
        S1_charge act;
        protected override act get_act() => act;
    }

    [path ("mecha")]
    public class do_S1 : acting.first {
        [link]
        S1 act;
        protected override act get_act() => act;
    }
}