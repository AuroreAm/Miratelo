using UnityEngine;

namespace Triheroes.Code {

    public class life : health_system.primary {

        HP4vessel red;
        HP4vessel black;

        public life ( int point_count ) {
            red = new HP4vessel ( point_count );
            black = new HP4vessel ( point_count );
        }

        public override void damage ( damage damage ) {
            
            int quarter_count = Mathf.FloorToInt ( ( damage.value + eps ) / HP4.one.value );

            quarter_count = red.damage ( quarter_count );
            black.damage ( quarter_count );
        }

        protected override void _step() {
            red.tick ( Time.deltaTime );
            black.tick ( Time.deltaTime );
        }

        public HP4 [] get_red => red.points;
        public HP4 [] get_black => black.points;

    }
}