using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [superstar]
    public class player : moon
    {
        protected override void _ready()
        {
            v = with ( new input ("Vertical", true) );
            h= with ( new input ("Horizontal", true) );
            mousex= with ( new input ("Mouse X", true) );
            mousey= with ( new input ("Mouse Y", true) );
            jump= with ( new input ("Action0") );
            action1= with ( new input ("Action1") );
            action2= with ( new input ("Action2") );
            action3= with ( new input ("Action3") );
            focus= with ( new input ("L2",true) );
            dash= with ( new input ("R2",true) );
            alt= with ( new input ("L1") );
            aim= with ( new input ("R1") );
            down= with ( new input ("DPadDown",true) );
        }

        public static Vector3 move => new Vector3(h.Raw,0, v.Raw);
        public static Vector2 delta_mouse => new Vector2(mousex.Raw, mousey.Raw);

        public static input v, h, mousex, mousey, jump, action1, action2, action3, focus, dash, alt, aim, down;
    }

    public sealed class input : bios
    {
        public static implicit operator bool ( input a ) => a.active;
        /// <summary> is the corresponding button held down </summary>
        bool active;
        /// <summary> is the corresponding button pressed down this frame </summary>
        public bool down => _down;
        /// <summary> is the corresponding button released this frame </summary>
        public bool up => _up;
        /// <summary> raw value of the corresponding button </summary>
        public float Raw => _is_axis ? Input.GetAxis ( _axis_name ) : Input.GetButton ( _axis_name )? 1f : 0f;

        bool _up;
        bool _down;
        bool _is_axis;
        string _axis_name;

        public input ( string input_manager_name, bool is_input_manager_axis = false )
        {
            _is_axis = is_input_manager_axis;
            _axis_name = input_manager_name;
        }

        protected override void _ready()
        {
            phoenix.core.execute (this);
        }

        protected override void _step()
        {
            _down = false;
            _up = false;

            if (active == false)
            {
                if ( (_is_axis && Mathf.Abs (Raw)>0.1f) || (!_is_axis && Input.GetButton ( _axis_name )) )
                {
                    _down = true;
                    active = true;
                }
            }
            else
            {
                if ( (_is_axis && Mathf.Abs (Raw)<0.1f) || (!_is_axis && !Input.GetButton ( _axis_name )) )
                {
                    _up = true;
                    active = false;
                }
            }
        }

    }
}
