using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class DirectCaptureWriter : Writer {
        protected override void __create() {
            Camera cam = GetComponent <Camera> ();
            cam.enabled = false;
            gameObject.SetActive (false);

            new direct_capture.ink ( cam );
        }
    }
}