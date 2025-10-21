using System.Collections.Generic;
using UnityEngine;
using Lyra;
using System.Data;

namespace Triheroes.Code {
    public class health : controller {
        public health_system.primary primary {get; private set;}
        health_system.sub [] systems = new health_system.sub [0];

        protected override void _ready() {
            phoenix.core.execute (this);
        }

        public void put_primary ( health_system.primary hs ) {
            if ( primary == null ) {
                primary = hs;
                link (hs);
            }
            else Debug.LogError ( "can't set primary twice" );
        }

        public void add ( health_system.sub hs ) {
            List <health_system.sub> temp = new List<health_system.sub> ( systems );

            health_system previous;
            if ( temp.Count > 0 ) 
            previous = temp [ temp.Count - 1 ];
            else previous = primary;

            temp.Add ( hs );
            hs.set_previous ( previous );
            link ( hs );

            systems = temp.ToArray (); 
        }

        public void damage ( damage damage ) {
            if ( systems.Length > 0 )
            systems [systems.Length - 1].damage ( damage );
            else 
            primary.damage ( damage );
        }
    }

    public abstract class health_system : auto_stat {
        public const float eps = .0001f;
        public abstract void damage ( damage damage );
        public health_system_hud hud {protected set; get;}

        private health_system () {}

        public abstract class sub : health_system {
            protected health_system previous;
            internal void set_previous ( health_system _previous ) {
                if ( previous == null )
                previous = _previous;
                else throw new InvalidExpressionException ( "can't set previous twice" );
            }
        }
        public abstract class primary : health_system {}
    }
}