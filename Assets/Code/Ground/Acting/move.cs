using Lyra;

namespace Triheroes.Code.Acting
{
    [path("acting")]
    public class move : acting.first {
        [link]
        Code.move move_act;

        protected override act get_act() => move_act;
    }

    [path("move")]
    public class set_move_point_speed : action {
        [link]
        move_point point;

        [export]
        public float speed = 7;

        protected override void _start() {
            point.speed = speed;
            stop ();
        }
    }
}