using UnityEngine.UI;
using Lyra;

namespace Triheroes.Code
{
    public class prompt : graphic_element {
        Text text;

        public prompt ( Text unity_text ) {
            text = unity_text;
        }
        
        public void start(string name) {
            text.text = name;

            if (!on) {
                ready_for_tick();
                phoenix.core.start_action(this);
            }
        }

        public void end() {
            if (on)
                stop();
        }

        protected override void _ready() {
            text.gameObject.SetActive(false);
        }

        protected override void _start() {
            text.gameObject.SetActive(true);
        }

        protected override void _stop() {
            text.gameObject.SetActive(false);
        }

    }
}
