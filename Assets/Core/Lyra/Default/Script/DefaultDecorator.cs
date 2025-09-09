namespace Lyra
{
    public sealed class sequence : decorator
    {
        int ptr;

        [export]
        public bool repeat = true ;
        [export]
        public bool reset = true ;

        protected sealed override void _start ()
        {
            if (reset)
            ptr = 0;
            o[ptr].tick ( this );
        }

        protected sealed override void _step ()
        {
            o [ptr].tick (this);
        }

        protected override void _abort()
        {
            if (o[ptr].on)
            o[ptr].stop (this);
        }

        public override void _star_stop(star p)
        {
            if (!on)
            return;

            ptr++;
            if (ptr >= o.Length)
            {
                ptr = 0;
                if (!repeat)
                {
                    stop();
                    return;
                }
            }

            o[ptr].tick ( this );
        }
    }

    public sealed class parallel : decorator
    {
        [export]
        public bool link_with_first = false;

        protected override void _start()
        {
            foreach (star p in o)
                p.tick ( this );
        }

        protected override void _step()
        {
            foreach (star p in o)
                if (p.on)
                    p.tick ( this );
        }

        public override void _star_stop(star p)
        {
            if (!on) return;

            if ( p == o [0] && link_with_first )
            {
                stop ();
                return;
            }

            foreach (var n in o)
                if (n.on)
                    return;

            stop ();
        }

        protected override void _stop()
        {
            foreach (var p in o)
                if (p.on)
                    p.stop (this);
        }
    }

}
