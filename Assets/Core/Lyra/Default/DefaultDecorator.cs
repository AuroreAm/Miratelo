namespace Lyra
{
    public sealed class chain : decorator
    {
        int ptr;

        [lyric]
        public bool repeat = true ;
        [lyric]
        public bool reset = true ;

        protected sealed override void awaken ()
        {
            if (reset)
            ptr = 0;
            o[ptr].sing ( this );
        }

        protected sealed override void alive ()
        {
            o [ptr].sing (this);
        }

        protected override void afaint()
        {
            if (o[ptr].on)
            o[ptr].halt (this);
        }

        public override void inhalt(aria p)
        {
            if (!on)
            return;

            ptr++;
            if (ptr >= o.Length)
            {
                ptr = 0;
                if (!repeat)
                {
                    sleep();
                    return;
                }
            }

            o[ptr].sing ( this );
        }
    }

    public sealed class chorus : decorator
    {
        [lyric]
        public bool LinkWithFirst = false;

        protected override void awaken()
        {
            foreach (aria p in o)
                p.sing ( this );
        }

        protected override void alive()
        {
            foreach (aria p in o)
                if (p.on)
                    p.sing ( this );
        }

        public override void inhalt(aria p)
        {
            if ( p == o [0] && LinkWithFirst )
            {
                sleep ();
                return;
            }

            foreach (var n in o)
                if (n.on)
                    return;

            sleep ();
        }

        protected override void asleep()
        {
            foreach (var p in o)
                if (p.on)
                    p.halt (this);
        }
    }

}
