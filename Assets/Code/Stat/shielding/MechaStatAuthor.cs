using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class MechaStatAuthor : ActorAuthorModule {
        public int CircuitHeart = 1;
        public int ShieldingHP = 600;
        public moon_paper < metal > Matter;

        public override void _create() {
            new ink <health> ();
        }

        public override void _created ( system s ) {
            var circuit = new circuit ( CircuitHeart ); s.add (circuit);
            var shield = new shielding ( ShieldingHP, Matter.write () ); s.add (shield);
            s.get <health> ().put_primary ( circuit );
            s.get <health> ().add ( shield );
        }
    }
}