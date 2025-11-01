using System.Collections.Generic;
using Lyra;

namespace Triheroes.Code
{
    [superstar]
    public class main_actors : moon {
        List <warrior> main = new List<warrior> ();
        public int count => main.Count;
        public warrior this [int id] => main [id];

        public void add ( warrior warrior ) {
            main.Add ( warrior );
            add_player_controller ( (actor) warrior );
        }

        void add_player_controller ( actor a ) {
            var script = a.system.get <script> ();

            var s = sb._a_all ( a.system );
                scr.add_player ( a, s );
            s._ ();

            script.add_or_change ( s.result (), sh.player_controller );
        }
    }
}
