using Lyra;

namespace Triheroes.Code.Element {
    public class skin : element, ruby <perce> {

        [link]
        health health;

        public void _radiate(perce gleam) {
            health.damage ( gleam.raw );
        }

        protected override void _ready() {
            register(system.get<character>().gameobject.GetInstanceID());
        }
        
    }
}