using Lyra;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triheroes.Code
{
    [superstar]
    public class player : moon {
        
        InputActionAsset main;

        protected override void _ready() {
            main = Resources.Load <InputActionAsset> ("input");
            main.Enable ();

            N = a_button ( "N" );
            S = a_button ( "S" );
            E = a_button ( "E" );
            W = a_button ( "W" );
            dash = a_button ( "dash" );
            _lock = a_button ( "lock" );
            aim = a_button ( "aim" );
            up = a_button ( "up" );
            left = a_button ( "left" );
            right = a_button ( "right" );
            down = a_button ( "down" );

            move1 = a_axis ( "move1" );
            move2 = a_axis ( "move2" );
        }

        button a_button ( string name ) {
            return with ( new button ( main.FindAction (name) ) );
        }

        axis a_axis ( string name ) {
            return with ( new axis ( main.FindAction (name) ) );
        }

        public static button N, S, E, W, dash, _lock, aim, up, left, right, down;
        public static axis move1, move2;
        public static Vector2 delta_mouse => move2.value2;
        public static Vector3 move => move1.value3;
    }

    public sealed class axis : moon {
        InputAction main;

        public axis ( InputAction a ) {
            main = a;
        }

        public Vector3 value3 => new Vector3 ( main.ReadValue <Vector2> ().x, 0, main.ReadValue <Vector2> ().y );
        public Vector3 value2 => main.ReadValue <Vector2> ();
    }

    public sealed class button : moon {

        InputAction main;

        public button ( InputAction a ) {
            main = a;
        }

        public static implicit operator bool ( button a ) => a.main.IsPressed ();
        public bool up => main.WasReleasedThisFrame ();
        public bool down => main.WasPressedThisFrame ();
    }
}
