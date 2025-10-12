using UnityEngine;

namespace Triheroes.Code {

    public class life : health_system {

        HP red;
        HP black;

        public life ( int point_count ) {
            red = new HP ( point_count );
            black = new HP ( point_count );
        }

        public override void damage ( float raw ) {
            int quarter_count = Mathf.FloorToInt ( raw / quarter.value );

            quarter_count = red.damage ( quarter_count );
            black.damage ( quarter_count );
        }

        protected override void _step() {
            red.tick ( Time.deltaTime );
            black.tick ( Time.deltaTime );
        }

        public life_point [] get_red => red.points;
        public life_point [] get_black => black.points;

        class HP {
            const float hot_duration = .5f;
            public life_point [] points;

            public HP ( int point_count ) {
                points = new life_point[point_count];
                for (int i = 0; i < points.Length; i++)
                    points [i].count = life_point.capacity;
            }

            public int quarter_count () {
                int count = 0;
                for (int i = 0; i < points.Length; i++) {
                    if (points [i].count == 0)
                        break;
                    count += points [i].count;
                }
                return count;
            }

            /// <returns> quarter_count rest </returns>
            public int damage ( int quarter_count ) {
                int count = quarter_count;
                for (int i = points.Length - 1; i >= 0 ; i--) {
                    if (points [i].count == 0)
                        continue;
                    else
                        count = points [i].damage ( count, hot_duration );
                }
                return count;
            }

            public void tick ( float dt ) {
                for (int i = 0; i < points.Length; i++)
                    points [i].tick ( dt );
            }
        }

    }

    public struct life_point {
        public const float value = 2;
        public const int capacity = 4;
        public int count;
        public quarter q0, q1, q2, q3;

        /// <returns> quarter_count rest </returns>
        public int damage (int quarter_count, float hot) {
            for ( int i = count - 1 ; i >= 0 && quarter_count > 0; i-- ) {
                quarter_count --;
                count --;
                switch (i) {

                    case 0:
                    q0.hot = hot;
                    break;

                    case 1:
                    q1.hot = hot;
                    break;

                    case 2:
                    q2.hot = hot;
                    break;

                    case 3:
                    q3.hot = hot;
                    break;
                }
            }

            return quarter_count;
        }

        public void tick ( float dt ) {
            q0.tick ( dt );
            q1.tick ( dt );
            q2.tick ( dt );
            q3.tick ( dt );
        }
    }

    public struct quarter {
        public const float value = 0.5f;
        public float hot;
        public bool is_hot => hot > 0;

        public void tick ( float dt ) {
            hot -= dt;
            if (hot < 0)
                hot = 0;
        }
    }

}