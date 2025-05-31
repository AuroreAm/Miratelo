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
        public Color BackgroundColor = new Color (.2667f, .3176f, .3412f);
        public Color NormalColor = new Color (0.35294117647058826f, 0.4235294117647059f, 0.42745098039215684f);
        public Color ContentColor = new Color (.26f, .31f, .34f);
        public Color BorderColor = new Color (.33f, .64f, .75f);

        public GUIStyle h1;
        public GUIStyle h2;
        public GUIStyle TextMiddleLeft;
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

            TextMiddle = new GUIStyle (Base);
            TextMiddle.alignment = TextAnchor.MiddleCenter;
        }
    }
}
