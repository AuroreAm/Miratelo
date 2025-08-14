using System;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

namespace Triheroes.Code
{
    public class Player : pix
    {
        public override void Create()
        {
            VMove = new InputAction ("Vertical", true);
            HMove= new InputAction ("Horizontal", true);
            MouseX= new InputAction ("Mouse X", true);
            MouseY= new InputAction ("Mouse Y", true);
            Jump= new InputAction ("Action0");
            Action1= new InputAction ("Action1");
            Action2= new InputAction ("Action2");
            Action3= new InputAction ("Action3");
            Focus= new InputAction ("L2",true);
            Dash= new InputAction ("R2",true);
            Alt= new InputAction ("L1");
            Aim= new InputAction ("R1");
            HatDown= new InputAction ("DPadDown",true);
        }

        public static Vector3 MoveAxis3 => new Vector3(HMove.Raw,0, VMove.Raw);
        public static Vector2 DeltaMouse => new Vector2(MouseX.Raw, MouseY.Raw);

        public static InputAction VMove, HMove, MouseX, MouseY, Jump, Action1, Action2, Action3, Focus, Dash, Alt, Aim, HatDown;
    }

    public sealed class InputAction : bios
    {

        /// <summary>
        /// is the corresponding button held down
        /// </summary>
        public bool Active { get; private set; }
        /// <summary>
        /// is the corresponding button pressed down this frame
        /// </summary>
        public bool OnActive => _OnDown;
        /// <summary>
        /// is the corresponding button released this frame
        /// </summary>
        public bool OnRelease => _OnUp;
        /// <summary>
        /// raw value of the corresponding button
        /// </summary>
        public float Raw => _IsAxis ? Input.GetAxis ( _InputManagerAccessName ) : Input.GetButton ( _InputManagerAccessName )? 1f : 0f;

        bool _OnUp;
        bool _OnDown;
        bool _IsAxis;
        string _InputManagerAccessName;

        public InputAction ( string InputManagerAccessName, bool IsInputManagerAccessNameAxis = false )
        {
            _IsAxis = IsInputManagerAccessNameAxis;
            _InputManagerAccessName = InputManagerAccessName;
            Stage.Start1 (this);
        }

        protected override void Step()
        {
            _OnDown = false;
            _OnUp = false;

            if (Active == false)
            {
                if ( (_IsAxis && Mathf.Abs (Raw)>0.1f) || (!_IsAxis && Input.GetButton ( _InputManagerAccessName )) )
                {
                    _OnDown = true;
                    Active = true;
                }
            }
            else
            {
                if ( (_IsAxis && Mathf.Abs (Raw)<0.1f) || (!_IsAxis && !Input.GetButton ( _InputManagerAccessName )) )
                {
                    _OnUp = true;
                    Active = false;
                }
            }
        }

    }
}