using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Editor
{
    public class Styles
    {
        public static Styles o
        {
            get 
            {
                if (_o== null)
                _o = new Styles();
                return _o;
            }
        }
        static Styles _o;
        public Color BackgroundColor = new Color (.00f, .05f, .0f);
        public Color NormalColor = new Color (.1f, .2f, .0f);
        public Color ContentColor = new Color (.0f, .1f, .0f);
        public Color BorderColor = new Color (.4f, .7f, .0f);

        public GUIStyle h1;
        public GUIStyle h2;
        public GUIStyle TextMiddleLeft;
        // with rich text
        public GUIStyle TextMiddleLeftRich;
        public GUIStyle TextMiddle;

        public GUIStyle Action;

        public Styles ()
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
            TextMiddleLeftRich = new GUIStyle (TextMiddleLeft);
            TextMiddleLeftRich.richText = true;

            Action = new GUIStyle (h2);
            Action.fontSize = 10;

            TextMiddle = new GUIStyle (Base);
            TextMiddle.alignment = TextAnchor.MiddleCenter;
        }
    }
}
