namespace Pixify.Spirit
{
    public sealed class sequence : decorator
    {
        int ptr;

        [Export]
        public bool repeat = true;
        [Export]
        public bool reset = true;

        protected sealed override void Start ()
        {
            if (reset)
            ptr = 0;
            o[ptr].Tick ( this );
        }

        protected sealed override void Step ()
        {
            o [ptr].Tick (this);
        }

        protected override void Abort()
        {
            if (o[ptr].on)
            o[ptr].ForceStop (this);
        }

        public override void OnPixiEnd(pixi p)
        {
            if (!on)
            return;

            ptr++;
            if (ptr >= o.Length)
            {
                ptr = 0;
                if (!repeat)
                {
                    SelfStop();
                    return;
                }
            }

            o[ptr].Tick ( this );
        }
    }

    public sealed class parallel : decorator
    {
        public bool LinkWithFirst = false;

        protected override void Start()
        {
            foreach (pixi p in o)
                p.Tick ( this );
        }

        protected override void Step()
        {
            foreach (pixi p in o)
                if (p.on)
                    p.Tick ( this );
        }

        public override void OnPixiEnd(pixi p)
        {
            if ( p == o [0] && LinkWithFirst )
            {
                SelfStop ();
                return;
            }

            foreach (var n in o)
                if (n.on)
                    return;

            SelfStop ();
        }

        protected override void Stop()
        {
            foreach (var p in o)
                if (p.on)
                    p.ForceStop (this);
        }
    }

}