using Lyra;

namespace Triheroes.Code.Element {
    public class skin : element, ruby<perce> {

        public void _radiate(perce gleam) {
            photon.radiate (new damage(gleam.raw));
        }

        protected override void _ready() {
            register(system.get<character>().gameobject.GetInstanceID());
        }
        
    }
}