using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Editor
{
    public class ScriptEditorStyles
    {
        public static ScriptEditorStyles o
        {
            get 
            {
                if (_o== null)
                _o = new ScriptEditorStyles();
                return _o;
            }
        }
        static ScriptEditorStyles _o;
        public Color BackgroundColor = new Color (.00f, .05f, .0f);
        public Color NormalColor = new Color (.1f, .2f, .0f);
        public Color ContentColor = new Color (.0f, .1f, .0f);
        public Color BorderColor = new Color (.4f, .7f, .0f);

        public GUIStyle h1;
        public GUIStyle h2;
        public GUIStyle TextMiddleLeft;
        // with rich text
        public GUIStyle TextMiddleLeftX;
        public GUIStyle TextMiddle;

        public ScriptEditorStyles ()
        {
            GUIStyle Base = new GUIStyle ("Label");

            h1 = new GUIStyle (Base);
            h1.fontStyle = FontStyle.Bold;
            h1.fontSize = 14;
            h1.normal.textColor = Color.white;

            h2 = new GUIStyle (Base);
            h2.fontStyle = FontStyle.Bold;
            h2.fontSize = 8;
            h2.normal.textColor = Color.white;
            
            TextMiddleLeft = new GUIStyle (Base);
            TextMiddleLeft.alignment = TextAnchor.MiddleLeft;
            TextMiddleLeftX = new GUIStyle (TextMiddleLeft);
            TextMiddleLeftX.richText = true;

            TextMiddle = new GUIStyle (Base);
            TextMiddle.alignment = TextAnchor.MiddleCenter;
        }
    }
}
