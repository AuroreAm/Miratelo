using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lyra.Editor
{
    
    namespace GUI
    {
        public class Box : Element
        {
            public Color ColorA;
            public Color ColorB;
            public float BorderWidth;

            public override void Draw()
            {
                EditorGUI.DrawRect(Transform, ColorA);
                DrawBorder(ColorB, BorderWidth);
            }
        }

        public class Label : Element
        {
            public string text;
            public GUIStyle style;

            public Label (string text, GUIStyle style)
            {
                this.text = text;
                this.style = style;
            }
            
            public override void Draw()
            {
                UnityEngine.GUI.Label (Transform, text, style);
            }

            public Label FitContent()
            {
                DefTransform.Size = style.CalcSize(new GUIContent(text));
                DefTransform.RelativeTransform = new Rect(0, 0, 0, 0);
                return this;
            }

            public Label FitContentW()
            {
                DefTransform.Size = new Vector2 (style.CalcSize(new GUIContent(text)).x,0);
                DefTransform.RelativeTransform = new Rect(0, 0, 0, 1);
                return this;
            }
        }
    }
}