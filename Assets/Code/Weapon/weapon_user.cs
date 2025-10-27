using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class weapon_user : controller
    {
        public abstract weapon weapon_base {get;}
        public abstract weapon_user _ ( weapon weapon );
    }

    [need_ready]
    public abstract class weapon_user <T> : weapon_user where T : weapon
    {
        [link]
        protected skin skin;

        [link]
        protected warrior warrior;

        public T weapon { get; protected set; }
        public override weapon weapon_base => weapon;

        public override weapon_user _ (weapon _weapon) => __ ( (T) _weapon);

        public weapon_user<T> __ (T _weapon) {
            if (on) {
                Debug.LogError ( "weapon user already on");
                return null;
            }

            weapon = _weapon;

            if (weapon == null)
                unready_for_tick();
            else
                ready_for_tick();

            return this;

        }

        protected sealed override void _start() {
            weapon.handle.aquire (warrior);
            __start ();
        }

        protected virtual void __start ()
        { }

        protected sealed override void _stop() {
            weapon.handle.dispose ();
            __stop ();
            weapon = null;
        }

        protected virtual void __stop ()
        { }
    }

    public abstract class weapon_user_standard <T> : weapon_user <T> where T : weapon
    {
        [link]
        protected hands hands;

        protected abstract int hand { get; }
        protected abstract int layer { get; }
        protected abstract Quaternion default_rotation { get; }

        protected sealed override void __start ()
        {
            var hands = ( Transform [] ) this.hands;
            weapon.coord.SetParent( hands [hand] );
            weapon.coord.localPosition = Vector3.zero;
            weapon.coord.localRotation = default_rotation;
            skin.enable_sync_layer(layer);
            _start ();
        }

        protected sealed override void __stop()
        {
            weapon.coord.SetParent(null);
            skin.disable_sync_layer(layer);
        }

        protected new virtual void _start ()
        { }
    }
}
