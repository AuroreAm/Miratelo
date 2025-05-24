using System;
using System.Collections.Generic;
using UnityEngine;

namespace Triheroes.Code
{
    // TODO migrate to new input system
    public static class Player
    {
        public static Vector3 LeftThumb;

        public static float GetAxis (string axis)
        {
            return Input.GetAxis (axis) + ( (String.Equals (axis, "Horizontal"))? LeftThumb.x : LeftThumb.z);
        }

        ///<summary> Get a horizontal input dir as Vector3 </summary>
        public static Vector3 GetAxis3 ()
        {
            return new Vector3(Player.GetAxis("Horizontal"), 0, Player.GetAxis("Vertical"));
        }

        public static Vector2 DeltaMouse()
        {
            return new Vector2 ( Player.GetAxis("Mouse X"),Player.GetAxis("Mouse Y") );
        }

        public static bool GetButton ( BoutonId button )
        {
            if (button == BoutonId.A)
            return Input.GetKey ( KeyCode.A );
            if (button == BoutonId._1)
            return Input.GetKey ( KeyCode.Alpha1 );
            if (button == BoutonId._2)
            return Input.GetKey ( KeyCode.Alpha2 );
            if (button == BoutonId._3)
            return Input.GetKey ( KeyCode.Alpha3 );
            else if (button == BoutonId.R)
            return Input.GetKey ( KeyCode.R );
            else if (button == BoutonId.E)
            return Input.GetKey ( KeyCode.E );
            else if (button == BoutonId.F)
            return Input.GetKey ( KeyCode.F );
            else
            return Input.GetButton ( (button == BoutonId.Jump)? "Jump": (button == BoutonId.Fire3)? "Fire3": (button == BoutonId.Fire1)? "Fire1": "Fire2" );
        }

        public static bool GetButtonDown (BoutonId button)
        {
            if (button == BoutonId.A)
            return Input.GetKeyDown ( KeyCode.A );
            if (button == BoutonId._1)
            return Input.GetKeyDown ( KeyCode.Alpha1 );
            if (button == BoutonId._2)
            return Input.GetKeyDown ( KeyCode.Alpha2 );
            if (button == BoutonId._3)
            return Input.GetKeyDown ( KeyCode.Alpha3 );
            else if (button == BoutonId.R)
            return Input.GetKeyDown ( KeyCode.R );
            else if (button == BoutonId.E)
            return Input.GetKeyDown ( KeyCode.E );
            else if (button == BoutonId.F)
            return Input.GetKeyDown ( KeyCode.F );
            else
            return Input.GetButtonDown ( (button == BoutonId.Jump)? "Jump": (button == BoutonId.Fire3)? "Fire3": (button == BoutonId.Fire1)? "Fire1": "Fire2" );
        }

        public static bool GetButtonUp (BoutonId button)
        {
            if (button == BoutonId.A)
            return Input.GetKeyUp ( KeyCode.A );
            if (button == BoutonId._1)
            return Input.GetKeyUp ( KeyCode.Alpha1 );
            if (button == BoutonId._2)
            return Input.GetKeyUp ( KeyCode.Alpha2 );
            if (button == BoutonId._3)
            return Input.GetKeyUp ( KeyCode.Alpha3 );
            else if (button == BoutonId.R)
            return Input.GetKeyUp ( KeyCode.R );
            else if (button == BoutonId.E)
            return Input.GetKeyUp ( KeyCode.E );
            else if (button == BoutonId.F)
            return Input.GetKeyUp ( KeyCode.F );
            else
            return Input.GetButtonUp ( (button == BoutonId.Jump)? "Jump": (button == BoutonId.Fire3)? "Fire3": (button == BoutonId.Fire1)? "Fire1": "Fire2" );
        }
    }

    // TODO: add a better way to handle input
    // TODO: rename the entire class to use French names
    public enum BoutonId {Jump,Fire3,Fire1,Fire2,E,A,R,F,_1,_2,_3};

}