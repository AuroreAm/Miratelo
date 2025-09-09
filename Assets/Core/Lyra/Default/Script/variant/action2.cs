using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public abstract class action2 : action
    {

        public enum states {a,b}
        protected states state { private set; get; }

        protected sealed override void _start()
        {
            state = states.a;
            _starta ();
        }

        protected sealed override void _step()
        {   
            switch (state)
            {
                case states.a : _stepa ();
                break;
                case states.b : _stepb ();
                break;
            }
        }

        protected virtual void _starta () {}
        protected virtual void _startb () {}

        protected virtual void _stepa() {}
        protected virtual void _stepb() {}

        protected virtual void _stopa () {}
        protected virtual void _stopb () {}

        protected void swap ()
        {
            switch (state)
            {
                case states.a :
                _stopa ();
                state = states.b;
                _startb ();
                break;
                case states.b :
                _stopb ();
                state = states.a;
                _starta ();
                break;
            }
        }

    }
}