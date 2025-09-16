using System;
using UnityEngine;

namespace Lyra
{
    public abstract class act : star.neutron
    {
        public abstract priority priority { get; }
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
            phoenix.core.execute (this);
        }

        protected override void _step()
        {
            if (act != null && act.on)
                act.tick(this);

            if (act2nd != null && act2nd.on)
                act2nd.tick(this);
        }

        bool replaced;
        public bool start_act (act _act, acting handler = null )
        {
            if ( _act.priority.level != 0 )
            {
                Debug.LogError ($"{_act} must have level 0");
                return false;
            }

            if (_act.priority <= priority) return false;
            
            if ( replaced )
            {
                Debug.LogError ("should not start act on act ending that was trigerred by higher priority");
                return false;
            }

            replaced = true;
            // TODO, if stopped here, should call handler or not?
            if (act != null)
            {
                act.stop(this);
            }

            act = _act;
            priority = act.priority;
            accept2nd = act.priority.accept2nd;

            acting = handler;

            if (!accept2nd && act2nd != null)
                act2nd.stop(this);

            replaced = false;
            act.tick(this);

            return true;
        }

        public void stop_act ( acting handler )
        {
            if (handler == acting)
                act.stop(this);
            else
                Dev.Break(" handler can't stop state ");
        }

        public bool start_act2nd ( act _act2nd, acting handler = null )
        {
            if ( _act2nd.priority.level != 1 )
            {
                Debug.LogError ($"{_act2nd} must have level 1");
                return false;
            }
            if (!accept2nd) return false;
            if (_act2nd.priority <= priority2nd) return false;
            
            if ( replaced )
            {
                Debug.LogError ("should not start act on act ending that was trigerred by higher priority");
                return false;
            }

            replaced = true;

            if (act2nd != null)
                act2nd.stop(this);

            acting2nd = handler;

            act2nd = _act2nd;
            priority2nd = _act2nd.priority;

            replaced = false;

            act2nd.tick(this );

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
                h._act_end (m, replaced);
        }

        void end2nd ()
        {
            var m = act2nd;
            var h = acting2nd;

            act2nd = null;
            acting2nd = null;
            priority2nd = -1;

            if ( h != null && h.on )
                h._act_end(m, replaced);
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
        public void _act_end ( act a, bool replaced );
        public bool on { get; }
    }

    public struct priority
    {
        public int value { private set; get; }
        public int level { private set; get; }
        public bool accept2nd { private set; get; }

        private priority ( int _value, int _level )
        {
            value = _value;
            level = _level;
            accept2nd = false;
        }

        public priority with2nd ()
        {
            accept2nd = true;
            return this;
        }

        public static implicit operator int ( priority p ) => p.value;

        public static readonly priority def = new priority (0, 0);
        public static readonly priority def2 = new priority (1, 0);
        public static readonly priority def3 = new priority (2, 0);
        public static readonly priority action = new priority (3, 0);
        public static readonly priority action2 = new priority (4, 0);
        public static readonly priority reaction = new priority (5, 0);
        public static readonly priority recovery = new priority (6, 0);

        public static readonly priority sub = new priority (2, 1);
    }

}