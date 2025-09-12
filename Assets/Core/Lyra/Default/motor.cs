using UnityEngine;

namespace Lyra
{
    public abstract class act : star.neutron
    {
        public abstract int priority { get; }
        public virtual bool accept2nd { get; } = false;
    }

    // motor layer of behavior
    // asign only state that directly manipulate motion of a character: animation and movement
    public sealed class motor : controller, core_kind
    {
        acting acting;

        acting acting2nd;

        public act act { get; private set; }
        public int priority { get; private set; } = -1;
        public bool accept2nd { get; private set; } = false;
        public act act2nd { get; private set; }
        public int priority2nd { get; private set; } = -1;

        protected override void _ready()
        {
            phoenix.core.start (this);
        }

        protected override void _step()
        {
            if (act != null && act.on)
                act.tick(this);

            if (act2nd != null && act2nd.on)
                act2nd.tick(this);
        }

        public bool start_act (act _act, acting handler = null )
        {
            if (_act.priority <= priority) return false;

            if (act != null)
                act.stop(this);

            act = _act;
            priority = act.priority;
            accept2nd = act.accept2nd;

            acting = handler;

            if (!accept2nd && act2nd != null)
                act2nd.stop(this);

            act.tick(this);
            return true;
        }

        public void stop_act ( acting handler )
        {
            if (handler == acting)
                act.stop(this);
            else
                Debug.LogError(" handler can't stop state ");
        }

        public bool start_act2nd ( act _act2nd, acting handler = null )
        {
            if (!accept2nd) return false;
            if (_act2nd.priority <= priority2nd) return false;

            if (act2nd != null)
                act2nd.stop(this);

            acting2nd = handler;

            act2nd = _act2nd;
            priority2nd = _act2nd.priority;

            act2nd.tick(this);
            return true;
        }

        /// <summary>
        /// end the main state at request of the original handler only
        /// </summary>
        /// <param name="handler"></param>
        public void stop_act2nd ( acting handler )
        {
            if (handler == acting2nd)
                act2nd.stop(this);
            else
                Debug.LogError(" handler can't stop state ");
        }

        void end ()
        {
            var m = act;
            var h = acting;

            act = null;
            acting = null;
            priority = -1;

            accept2nd = false;

            if ( h != null && h.on )
                h._act_end (m);
        }

        void end2nd ()
        {
            var m = act2nd;
            var h = acting2nd;

            act2nd = null;
            acting2nd = null;
            priority2nd = -1;

            if ( h != null && h.on )
                h._act_end(m);
        }

        public void _star_stop ( star s )
        {
            if (s == act)
                end();

            if (s == act2nd)
                end2nd();
        }
    }


    public interface acting
    {
        public void _act_end ( act a );
        public bool on { get; }
    }

}