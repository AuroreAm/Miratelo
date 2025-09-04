namespace Lyra
{
    public sealed class sequence : decorator
    {
        int ptr;

        [Export]
        public bool Repeat = true ;
        [Export]
        public bool Reset = true ;

        protected sealed override void OnStart ()
        {
            if (Reset)
            ptr = 0;
            o[ptr].Tick ( this );
        }

        protected sealed override void OnStep ()
        {
            o [ptr].Tick (this);
        }

        protected override void OnAbort()
        {
            if (o[ptr].on)
            o[ptr].ForceStop (this);
        }

        public override void OnSysEnd(sys p)
        {
            if (!on)
            return;

            ptr++;
            if (ptr >= o.Length)
            {
                ptr = 0;
                if (!Repeat)
                {
                    Stop();
                    return;
                }
            }

            o[ptr].Tick ( this );
        }
    }

    public sealed class parallel : decorator
    {
        [Export]
        public bool LinkWithFirst = false;

        protected override void OnStart()
        {
            foreach (sys p in o)
                p.Tick ( this );
        }

        protected override void OnStep()
        {
            foreach (sys p in o)
                if (p.on)
                    p.Tick ( this );
        }

        public override void OnSysEnd(sys p)
        {
            if ( p == o [0] && LinkWithFirst )
            {
                Stop ();
                return;
            }

            foreach (var n in o)
                if (n.on)
                    return;

            Stop ();
        }

        protected override void OnStop()
        {
            foreach (var p in o)
                if (p.on)
                    p.ForceStop (this);
        }
    }

}
