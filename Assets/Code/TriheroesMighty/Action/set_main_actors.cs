using Lyra;

namespace Triheroes.Code
{
    [path("scene")]
    public class set_main_actors : action {
        [link]
        main_actors a;

        [export]
        public string [] main_actors;

        protected override void _start () {
            foreach (string s in main_actors)
            a.add ( xenos.get_actor (new term (s)).system.get <warrior> () );
            
            stop ();
        }
    }
}