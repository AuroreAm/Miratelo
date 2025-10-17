using System.Collections.Generic;
using Lyra;
using Triheroes.Code;

namespace Triheroes.Code {
    public static partial class scr {
        public static void add_actor(script_builder sb) {
            sb.a<react_fall_hard>();
        }
    }
}