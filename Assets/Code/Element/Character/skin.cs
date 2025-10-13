using Lyra;

namespace Triheroes.Code.Element {
    public class skin : element, ruby <damage> {

        [link]
        health health;

        public void _radiate(damage gleam) {
            health.damage ( gleam.value );
        }

        protected override void _ready() {
            register(system.get<character>().gameobject.GetInstanceID());
        }
        
    }
}