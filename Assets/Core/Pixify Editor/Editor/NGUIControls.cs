using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pixify.Editor
{
    namespace NGUI
    {
        
        /// <summary>
        /// Area automatically set height to the grid children, don't add elements that rely on height of this
        /// </summary>
        public class AreaList : Area
        {
            public float PaddingLeft;
            public float PaddingRight;
            public float y;

            public AreaList (float paddingLeft, float paddingRight, float y)
            {
                PaddingLeft = paddingLeft;
                PaddingRight = paddingRight;
                this.y = y;
            }

            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                return new Vector2 ( PaddingLeft, y );
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                Vector2 defSize = new Vector2 ( DefParentSize.x - PaddingLeft - PaddingRight, 0 );

                float h = 0;
                for (int i = 0; i < Children.Count; i++)
                {
                    Children [i].SetDefSize ( defSize );
                    Children [i].SetDefPos ( defSize );
                    Children [i].TweakDefPos ( new Vector2 (0,h) );
                    h += Children [i].DefRect.height;
                }

                defSize = new Vector2 ( DefParentSize.x - PaddingLeft - PaddingRight, h );
                SetDefSizeChildren (defSize);

                return defSize;
            }
        }

        public class AreaRow : Area
        {
            public float y;
            public float height;

            public AreaRow (float y, float height)
            {
                this.y = y;
                this.height = height;
            }

            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                return new Vector2 (0, y);
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                Vector2 defSize = new Vector2 (0,height);

                float w = 0;
                for (int i = 0; i < Children.Count; i++)
                {
                    Children [i].SetDefSize ( defSize );
                    Children [i].SetDefPos ( defSize );
                    Children [i].TweakDefPos ( new Vector2 (w,0) );
                    w += Children [i].DefRect.width;
                }

                defSize = new Vector2 (w,height);
                SetDefSizeChildren (defSize);

                return defSize;
            }
        }

        public class Scroll : ELementFull
        {
            Element Main;
            public Vector2 spos;

            public Scroll ( Element main )
            {
                Main = main;
            }

            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                var v = base.GetDefPos(DefParentSize);
                Main.SetDefPos (v);
                return v;
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                var v = base.GetDefSize(DefParentSize);
                Main.SetDefSize (v);
                return v;
            }

            internal override void InitRect()
            {
                base.InitRect();
                Main.InitRect();
            }

            public override void Draw()
            {
                spos = GUI.BeginScrollView (Rect, spos, Main.DefRect);
                Main.Draw ();
                GUI.EndScrollView ();
            }
        }

        public class IMGUI : ELementFull
        {
            public Action GUI;

            public IMGUI ( Action gui )
            {
                GUI = gui;
            }

            public override void Draw()
            {
                GUI ();
            }
        }

        public class ColorFull : ELementFull
        {
            Color color;

            public ColorFull (Color color)
            {
                this.color = color;
            }

            public override void Draw()
            {
                EditorGUI.DrawRect (Rect, color);
            }
        }

        /// <summary>
        /// color covering the whole area, with an accent on top
        /// </summary>
        public class ColorFullA : ColorFull
        {
            Color accent;

            public ColorFullA (Color color) : base (color)
            {
                accent = color * 2;
            }

            public override void Draw()
            {
                base.Draw ();
                // draw one line on top of the color
                EditorGUI.DrawRect (new Rect (Rect.x, Rect.y, Rect.width, 2), accent);
            }
        }

        /// <summary>
        /// color covering the whole area, with borders
        /// </summary>
        public class ColorFullB : ColorFull
        {
            Color borderColor;

            public ColorFullB (Color color, Color border) : base (color)
            {
                this.borderColor = border;
            }

            public override void Draw()
            {
                base.Draw ();
                DrawBorder (borderColor, 1);
            }
        }

        public class BackgroundList : ELementFull
        {
            float GridHeight;

            public BackgroundList (float gridHeight)
            {
                GridHeight = gridHeight;
            }

            public override void Draw()
            {
                // draw a vertical grid line every GridHeight
                for (float i = 0; i < Rect.height; i += GridHeight)
                {
                    EditorGUI.DrawRect(new Rect(0, i, Rect.width, 1), Color.black);
                }
            }
        }

        public class Label : Element
        {
            public string text;
            public GUIStyle style;
            public Vector2 position;
            public Label (Vector2 position, string text, GUIStyle style)
            {
                this.position = position;
                this.text = text;
                this.style = style;
            }

            public override void Draw()
            {
                GUI.Label (Rect, text, style);
            }

            protected override Vector2 GetDefPos(Vector2 DefParentSize) => position;

            protected override Vector2 GetDefSize(Vector2 DefParentSize) => style.CalcSize(new GUIContent(text));
        }

        public class ButtonToggle : ELementFull
        {
            public string text;
            public GUIStyle style;
            public bool on;
            Color ColorOn; Color ColorOff;
            public Action OnClick;

            public ButtonToggle(string text, GUIStyle style, Color ColorOn, Color ColorOff)
            {
                this.text = text;
                this.style = style;
                this.ColorOn = ColorOn;
                this.ColorOff = ColorOff;
            }

            public override void Draw()
            {
                EditorGUI.DrawRect(Rect, on ? ColorOn : ColorOff);
                if (GUI.Button ( Rect, text, style ))
                OnClick?.Invoke ();
            }
        }
    }
}