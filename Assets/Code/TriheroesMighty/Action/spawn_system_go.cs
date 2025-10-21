using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [path ("scene")]
    public class spawn_system_go : action {
        protected override void _start() {
            res.go.instantiate ( sh.camera ).GetComponent <Writer> ().Write ();
            res.go.instantiate ( sh.direct_capture ).GetComponent <Writer> ().Write ();
            res.go.instantiate ( sh.hud ).GetComponent <Writer> ().Write ();
            
            stop ();
        }
    }
}