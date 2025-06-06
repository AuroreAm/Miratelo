using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pixify.Editor
{
    namespace PixGUI
    {
        // Unity IMGUI wrapper
        public class AreaScreen
        {
            public Area Root;

            public AreaScreen ( Area root )
            {
                Root = root;
            }

            public void Draw ()
            {
                Root.ResetRect ();
                Root.InitRect ( new Vector2 ( Screen.width, Screen.height - 20 ), new DefTransform() { RelativeTransform = new Rect(0, 0, 1, 1) } );
                Root.Draw ();
            }
        }

        public class DirectArea
        {
            public Area Root;
            public DirectArea ( Area root )
            {
                Root = root;
            }
            
            public void Draw( float width, float height )
            {
                Root.ResetRect ();
                Root.InitRect ( new Vector2 ( width, height - 20 ), new DefTransform() { RelativeTransform = new Rect(0, 0, 1, 1) } );
                Root.Draw ();
            }
        }
        

        public struct DefTransform
        {
            public Rect RelativeTransform;
            public Vector2 Position;
            public Vector2 Size;
            public Vector4 Padding;
        }

        public abstract class Element
        {
            public abstract void Draw();
            public Rect Transform { get; private set; }
            public DefTransform DefTransform = new DefTransform() { RelativeTransform = new Rect(0, 0, 1, 1) };
            public bool on = true;

            public virtual void ResetRect () { Transform = new Rect(); }

            public void InitRect ( Vector2 ParentSize, DefTransform ParentDefTransform ) {
                Vector2 size = GetInitSize ( ParentSize, ParentDefTransform );
                Vector2 pos = GetInitPos ( ParentSize );
                Transform = new Rect ( pos, size );
            }

            protected virtual Vector2 GetInitPos ( Vector2 ParentSize ) => DefaultInitPos(ParentSize);

            protected virtual Vector2 GetInitSize ( Vector2 ParentSize, DefTransform ParentDefTransform ) => DefaultInitSize(ParentSize);

            public Vector2 DefaultInitPos(Vector2 ParentSize)
            {
                Vector2 pos = DefTransform.Position;
                pos += DefTransform.RelativeTransform.position * ParentSize;
                pos += DefTransform.Padding.xy ();
                return pos;
            }

            public Vector2 DefaultInitSize(Vector2 ParentSize)
            {
                Vector2 size = DefTransform.Size;
                size += DefTransform.RelativeTransform.size * ParentSize;
                size -= DefTransform.Padding.zw () + DefTransform.Padding.xy ();
                return size;
            }

            public void DrawBorder (Color borderColor, float borderWidth)
            {
                Rect rect = Transform; 
                if (borderWidth <= 0) return;
                borderWidth = Mathf.Min(borderWidth, rect.width / 2f, rect.height / 2f);
                EditorGUI.DrawRect(new Rect(rect.xMin, rect.yMin, rect.width, borderWidth), borderColor);
                EditorGUI.DrawRect(new Rect(rect.xMin, rect.yMax - borderWidth, rect.width, borderWidth), borderColor);
                EditorGUI.DrawRect(new Rect(rect.xMin, rect.yMin + borderWidth, borderWidth, rect.height - 2 * borderWidth), borderColor);
                EditorGUI.DrawRect(new Rect(rect.xMax - borderWidth, rect.yMin + borderWidth, borderWidth, rect.height - 2 * borderWidth), borderColor);
            }

            public Element RelativeTransform(Rect rect)
            {
                DefTransform.RelativeTransform = rect;
                return this;
            }

            public Element Position(Vector2 pos)
            {
                DefTransform.Position = pos;
                return this;
            }

            public Element Padding(Vector4 padding)
            {
                DefTransform.Padding = padding;
                return this;
            }

            public Element Size (Vector2 size)
            {
                DefTransform.Size = size;
                return this;
            }
        }

        public class Area : Element
        {
            public List<Element> Children = new List<Element> ();

            override sealed public void ResetRect ()
            { base.ResetRect (); foreach (var e in Children) e.ResetRect (); }

            public Area ( params Element[] E )
            {
                Add ( E );
            }

            public void Add ( params Element[] E )
            {
                foreach (var e in E)
                Children.Add (e);
            }

            public void Insert ( Element e, int index )
            {
                if (index < Children.Count)
                Children.Insert (index, e);
                else Children.Add (e);
            }

            public void Remove ( Element e )
            {
                Children.Remove (e);
            }

            public int IndexOf ( Element e )
            {
                return Children.IndexOf (e);
            }

            public void Clear ()
            {
                Children.Clear ();
            }

            public override void Draw()
            {
                GUILayout.BeginArea ( Transform );
                int ChildrenCount = Children.Count;

                for (int i = 0; i < ChildrenCount; i++)
                {
                    if (Children [i].on)
                    Children [i].Draw ();

                    if (ChildrenCount != Children.Count)
                        break;
                }
                GUILayout.EndArea ();
            }

            override protected Vector2 GetInitPos ( Vector2 ParentSize )
            {
                return base.GetInitPos ( ParentSize );
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                Vector2 size = base.GetInitSize(ParentSize, ParentDefTransform);
                InitRectChildren ( size, DefTransform );
                return size;
            }

            protected void InitRectChildren ( Vector2 ParentSize , DefTransform ParentDefTransform )
            {
                foreach (var e in Children)
                if (e.on)
                e.InitRect ( ParentSize, ParentDefTransform );
            }
        }
    }
}
