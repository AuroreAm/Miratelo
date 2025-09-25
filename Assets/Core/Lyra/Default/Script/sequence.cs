using System;

namespace Lyra
{
    public sealed class sequence : decorator
    {
        int ptr;

        [export]
        public bool repeat = true ;
        [export]
        public bool reset = true ;

        public static sequence new_sequence ( action[] o ){
            set_constructor_event ( s => ((sequence)s).set ( o ) );
            return new sequence ();
        }

        protected sealed override void _start ()
        {
            if (reset)
            ptr = 0;
            o[ptr].tick ( this);
        }

        protected sealed override void _step ()
        {
            o [ptr].tick (this);
        }

        protected override void _abort()
        {
            if (o[ptr].on)
            o[ptr].abort (this);
        }

        // TODO NOTE sequence act like a while loop, if all child directly stop at start, it triggers stack overflow

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
}
