namespace Lyra
{
    [inked]
    public class acting : parallel, act_handler {
        
        public act _act;

        act act;

        [link]
        motor motor;

        protected override void __ready() {
            system.add (_act);
            act = _act;
        }

        // NOTE if starting was failed, the action stops, check on before continuing operation after stop
        // TODO task failed
        public void _act_end( act a, act_status status ) {
            if ( a == act )
            stop ();
        }

        public override void _star_stop(star s) {}

        // TODO bool can start
        protected override void _start() {
            motor.start_act ( act );

            if (!on)
            return;

            base._start();
        }
    }
}
