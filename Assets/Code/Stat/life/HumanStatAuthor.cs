using Lyra;

namespace Triheroes.Code
{
    public class HumanStatAuthor : ActorAuthorModule {
        public int Heart = 1;
        public int Stamina = 7;

        life l;
        public override void _create() {

            new ink <health> ();
            new stamina.ink ( Stamina );

            l = new life ( Heart );
            
        }

        public override void _created(system s) {
            s.get <health> ().put_primary (l);
        }
    }
}